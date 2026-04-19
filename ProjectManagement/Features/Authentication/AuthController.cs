using MediatR;
using Microsoft.AspNetCore.Authentication;
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
    [ApplicationModule(Module.User)]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost]
        [Route("Login")]
        public AuthenticateResponse Login(AuthenticateRequest model)
        {
            return this.authenticationService.Authenticate(model);

        }
    }
}
