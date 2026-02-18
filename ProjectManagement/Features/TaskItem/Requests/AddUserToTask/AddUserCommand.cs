using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public record AddUserCommand(Guid UserId, string Publisher, Guid TaskId) : IRequest<AddUserResult>;
}
