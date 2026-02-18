using MediatR;

namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public record AddUserCommand(Guid UserId, string Publisher, Guid ProjectId) : IRequest<AddUserResult>;
}
