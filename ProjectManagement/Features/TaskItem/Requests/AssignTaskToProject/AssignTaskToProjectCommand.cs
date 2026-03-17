using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject
{
    public record AssignTaskToProjectCommand(Guid TaskId, Guid ProjectId) : IRequest<AssignTaskToProjectResult>;
}
