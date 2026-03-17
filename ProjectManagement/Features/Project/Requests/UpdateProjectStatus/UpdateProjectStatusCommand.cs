using MediatR;

namespace ProjectManagement.Features.Project.Requests.UpdateProjectStatus
{
    public record UpdateProjectStatusCommand(
        Guid ProjectId,
        Guid StatusId
    ) : IRequest;
}