using MediatR;
using ProjectManagement.Features.TaskItem.Request.AssignTaskToProject;

namespace ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject
{
    public record AddProjectCommand(Guid TaskId, Guid ProjectId) : IRequest<AddProjectResult>;
}
