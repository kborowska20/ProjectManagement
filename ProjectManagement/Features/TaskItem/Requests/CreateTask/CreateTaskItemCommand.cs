using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public record CreateTaskItemCommand(
        string Title,
        string Description,
        Guid ProjectId,
        Guid AssignedUserId
    ) : IRequest<Domain.TaskItem>;
}