using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Weinmann.Core.Repositories;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Customer;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationService(IRepository<Customer> customerRepository, IMapper mapper, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<CustomerDTO> Register(CustomerRegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
                throw new ArgumentNullException(nameof(registrationDTO));

            if (string.IsNullOrEmpty(registrationDTO.UserName))
                throw new ArgumentNullException($"UserName \\ {registrationDTO.UserName} \\ is required");

            if (string.IsNullOrEmpty(registrationDTO.Password))
                throw new ArgumentNullException($"Password \\ {registrationDTO.Password} \\ is required");

            if(await _customerRepository.FindByConditionAsync(data => data.UserName == registrationDTO.UserName) != null)
                throw new ArgumentNullException($"Customer with \\ {registrationDTO.UserName} \\ is already registered");

            CreatePasswordHash(registrationDTO.Password, out var passwordHash, out var passwordSalt);

            var newCustomer = new Customer {
                UserName = registrationDTO.UserName,
                BusinessLocationId = registrationDTO.BusinessLocationId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _customerRepository.AddAsync(newCustomer);
            await _customerRepository.SaveChangesAsync();

            var result = _mapper.Map<CustomerDTO>(newCustomer);

            return result;
        }

        public async Task<JwtSecurityToken> Authenticate(CustomerAuthenticationDTO authenticationDTO)
        {
            if (string.IsNullOrEmpty(authenticationDTO.UserName) || string.IsNullOrEmpty(authenticationDTO.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            var userCredentials = await _customerRepository.FindByConditionAsync(data => data.UserName == authenticationDTO.UserName);

            if (userCredentials == null)
                throw new UnauthorizedAccessException("Invalid email or password."); ;

            if (!VerifyPasswordHash(authenticationDTO.Password, userCredentials.PasswordHash, userCredentials.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid email or password."); ;

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, authenticationDTO.UserName)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                    issuer: _configuration["authOptions:issuer"],
                    audience: _configuration["authOptions:audience"],
                    notBefore: DateTime.UtcNow,
                    claims: claimsIdentity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(_configuration["authOptions:lifeTimeInMinutes"]))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["authOptions:key"])), SecurityAlgorithms.HmacSha256));

            return jwt;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
