using MediatR;

namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public record AddUserToProjectCommand(Guid UserId, Guid ProjectId) : IRequest<AddUserToProjectResult>;
}
