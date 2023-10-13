using System.IdentityModel.Tokens.Jwt;
using Weinmann.Domain.DTOs;

namespace Weinmann.Core.Services
{
    public interface IAuthenticationService
    {
        public Task<CustomerDTO> Register(CustomerRegistrationDTO registrationDTO);
        public Task<JwtSecurityToken> Authenticate(CustomerAuthenticationDTO registrationDTO);
    }
}
