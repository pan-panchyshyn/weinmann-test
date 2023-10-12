using System.IdentityModel.Tokens.Jwt;
using Weinmann.Domain.DTOs;

namespace Weinmann.Core.Services
{
    public interface IAuthenticationService
    {
        public Task<CustomerDTO> Register(RegistrationDTO registrationDTO);
        public Task<JwtSecurityToken> Authenticate(RegistrationDTO registrationDTO);
    }
}
