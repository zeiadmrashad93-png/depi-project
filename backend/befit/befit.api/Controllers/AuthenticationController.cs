using befit.application.Contracts;
using befit.application.DTOs.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace befit.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            string? responseToken = await _authenticationService.AuthenticateUser(dto);
            if (responseToken == null)
                return Unauthorized();

            return Ok(responseToken);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            string? responseToken = await _authenticationService.RegisterNewUser(dto);
            if (responseToken == null)
                return BadRequest();

            return Ok(responseToken);
        }
    }
}
