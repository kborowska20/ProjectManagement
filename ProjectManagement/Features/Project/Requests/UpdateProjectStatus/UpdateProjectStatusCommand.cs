using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public record UpdateProjectStatusCommand(
        Guid ProjectId,
        Guid StatusId
    ) : IRequest;
}