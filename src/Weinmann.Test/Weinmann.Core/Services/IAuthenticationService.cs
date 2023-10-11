using Weinmann.Domain.DTOs;
using Weinmann.Domain.Models;

namespace Weinmann.Core.Services
{
    public interface IAuthenticationService
    {
        public Task<Customer> Register(RegistrationDTO registrationDTO);
    }
}
