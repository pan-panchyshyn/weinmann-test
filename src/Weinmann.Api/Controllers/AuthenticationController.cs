using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Customer;

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
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationDTO registrationDTO)
        {
            try
            {
                await _authenticationService.Register(registrationDTO);
                var encodedJwt = await _authenticationService.Authenticate(new CustomerAuthenticationDTO 
                {
                    UserName = registrationDTO.UserName,
                    Password = registrationDTO.Password
                });

                var response = new
                {
                    user_name = registrationDTO.UserName,
                    access_token = encodedJwt,
                    token_lifeTime = 3600000
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] CustomerAuthenticationDTO registrationDTO)
        {
            try
            {
                var encodedJwt = await _authenticationService.Authenticate(registrationDTO);

                var response = new
                {
                    user_name = registrationDTO.UserName,
                    access_token = encodedJwt,
                    token_lifeTime = 3600000
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
