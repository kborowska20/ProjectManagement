using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Attributes;
using ProjectManagement.Enums;
using ProjectManagement.Features.User.Requests.CreateUser;
using ProjectManagement.Features.User.Requests.GetUser;
using ProjectManagement.Features.User.Requests.UpdateUserRole;

namespace ProjectManagement.Features.User
{
    [ApiController]
    [Route("[controller]")]
    [ApplicationModule(Module.User)]
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Domain.User>> CreateUser(CreateUserCommand command)
        {
            var user = await _mediator.Send(command);

            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserRole(UpdateUserRoleCommand command)
        {
            if (command.UserId != command.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetUserResult>> GetUser(Guid id)
        {
            var command = new GetUserQuery(id);
            var user = await _mediator.Send(command);

            if (user == null)
            {
                return NotFound($"User with ID not found.");
            }

            return Ok(user);
        }
    }
}
