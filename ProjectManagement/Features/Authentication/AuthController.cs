using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Attributes;
using ProjectManagement.Enums;
using ProjectManagement.Features.User.Requests.CreateUser;
using ProjectManagement.Features.User.Requests.GetUser;
using ProjectManagement.Features.User.Requests.UpdateUserRole;

namespace ProjectManagement.Features.Authentication
{
    [ApiController]
    [Route("[controller]")]
    [ApplicationModule(Module.Authentication)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateRequest model)
        {
            var response = _authenticationService.Authenticate(model);

            if (response == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var user = HttpContext.Items["User"];

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            return Ok(new { message = "Token is valid", user });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var user = HttpContext.Items["User"];

            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            return Ok(user);
        }
    }
}
