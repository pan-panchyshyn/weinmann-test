using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs;

namespace Weinmann.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
        {
            await _authenticationService.Register(registrationDTO);
            return Ok();
        }
    }
}
