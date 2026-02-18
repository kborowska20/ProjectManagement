using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.Project.Requests.GetProject;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetProject(Guid id)
        {

            var query = new GetProjectQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult> AssignUserToProject(AddUserCommand userCommand)
        {

            var project = await _mediator.Send(userCommand);

            if (project == null)
            {
                return NotFound($"Project with ID {userCommand.ProjectId} not found.");
            }

            return Ok(project);
        }
    }
}
