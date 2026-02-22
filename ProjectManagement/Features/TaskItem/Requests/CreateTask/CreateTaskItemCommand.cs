using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public record UpdateUserRoleCommand(
        string TaskName,
        string Description,
        Guid ProjectId,
        DateTime? DueDate = null
    ) : IRequest<Domain.TaskItem>;
}