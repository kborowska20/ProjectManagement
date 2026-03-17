namespace ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject
{
    public record AssignTaskToProjectResult(
        Guid TaskId,
        Guid ProjectId,
        string TaskTitle
    );
}
