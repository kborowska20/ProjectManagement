using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.Project.Requests.GetProject;
using ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;
using ProjectManagement.Features.Project.Requests.UpdateProjectStatus;

namespace ProjectManagement.Features.Project
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
        public async Task<ActionResult<GetProjectResult>> GetProject(Guid id)
        {
            var query = new GetProjectQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(project);
        }

        [HttpPost("assignUserToProject")]
        public async Task<ActionResult<AddUserToProjectResult>> AssignUserToProject(AddUserToProjectCommand userToProjectCommand)
        {
            var result = await _mediator.Send(userToProjectCommand);

            if (result == null)
            {
                return NotFound($"Project with ID {userToProjectCommand.ProjectId} not found.");
            }

            return Ok(result);
        }

        [HttpPut("updateProjectStatus")]
        public async Task<ActionResult> UpdateProjectStatus(UpdateProjectStatusCommand updateProjectStatusCommand)
        {
            await _mediator.Send(updateProjectStatusCommand);
            return NoContent();
        }

        [HttpDelete("deleteTaskFromProject")]
        public async Task<ActionResult> DeleteTaskFromProject([FromQuery] Guid projectId, [FromQuery] Guid taskId)
        {
            var command = new RemoveTaskFromProjectCommand(projectId, taskId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
