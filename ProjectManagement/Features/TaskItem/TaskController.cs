using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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
        public async Task<ActionResult> GetProject(Guid id)
        {

            var query = new GetTaskQuery(id);
            var task = await _mediator.Send(query);

            if (task == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return Ok(task);
        }

    }
}
