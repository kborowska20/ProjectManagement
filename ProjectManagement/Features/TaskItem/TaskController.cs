using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Features.TaskItem.Requests.AddUserToProject;
using ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject;
using ProjectManagement.Features.TaskItem.Requests.CreateTask;
using ProjectManagement.Features.TaskItem.Requests.GetTask;

namespace ProjectManagement.Features.TaskItem
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetTaskResult>> GetTask(Guid id)
        {
            var query = new GetTaskQuery(id);
            var task = await _mediator.Send(query);

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.TaskItem>> CreateTask(CreateTaskItemCommand command)
        {
            var task = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                task
            );
        }

        [HttpPost("assignUser")]
        public async Task<ActionResult> AssignTaskToUser(AddTaskToUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("assignProject")]
        public async Task<ActionResult> AssignTaskToProject(AssignTaskToProjectCommand command)
        {

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
