using MediatR;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public record AddTaskToUserCommand(Guid TaskItemId, Guid UserId) : IRequest<AddTaskToUserResult>;
}
