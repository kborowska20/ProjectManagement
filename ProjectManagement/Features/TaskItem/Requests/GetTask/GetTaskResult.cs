using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Requests.GetTask
{
    public record GetTaskResult(
        Guid Id,
        string Title,
        string Description
    );
}
