using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public record AddTaskToUserCommand(Guid UserId, Guid TaskId) : IRequest<AddProjectResult>;
}
