using MediatR;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public record AddTaskToUserCommand(Guid TaskId,Guid UserId) : IRequest<AddTaskToUserResult>;
}
