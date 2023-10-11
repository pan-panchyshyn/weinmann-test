using System.Text;
using Weinmann.Core.Repositories;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Customer> _customerRepository;

        public AuthenticationService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Register(RegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
                throw new ArgumentNullException(nameof(registrationDTO));

            if (string.IsNullOrEmpty(registrationDTO.UserName))
                throw new ArgumentNullException($"Username \\ {registrationDTO.UserName} \\ is requred");

            if (string.IsNullOrEmpty(registrationDTO.Password))
                throw new ArgumentNullException($"Passord \\ {registrationDTO.Password} \\ is requred");

            if(await _customerRepository.FindByConditionAsync(data => data.UserName == registrationDTO.UserName) != null)
                throw new ArgumentNullException($"Customer with \\ {registrationDTO.UserName} \\ is already registered");

            CreatePasswordHash(registrationDTO.Password, out var passwordHash, out var passwordSalt);

            var newCustomer = new Customer(registrationDTO.UserName, passwordHash, passwordSalt);

            await _customerRepository.Add(newCustomer);
            await _customerRepository.SaveChangesAsync();

            return newCustomer;
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
    }
}
