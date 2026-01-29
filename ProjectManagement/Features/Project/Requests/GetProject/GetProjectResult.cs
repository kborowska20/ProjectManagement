using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Requests.GetProject
{
    public record GetProjectResult(
        Guid Id,
        string ProjectName,
        string Description,
        ProjectStatus Status,
        IEnumerable<Domain.User> Users,
        IEnumerable<TaskItem> Tasks
    );
}
