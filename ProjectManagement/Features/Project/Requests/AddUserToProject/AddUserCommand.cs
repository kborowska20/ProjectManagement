using MediatR;

namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public record AddUserCommand(string Name, string Publisher, int ProjectId) : IRequest<AddUserResult>;
}
