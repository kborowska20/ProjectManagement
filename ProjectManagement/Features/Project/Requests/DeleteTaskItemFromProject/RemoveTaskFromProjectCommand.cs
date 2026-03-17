using MediatR;

namespace ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;

public record RemoveTaskFromProjectCommand(Guid ProjectId, Guid TaskId) : IRequest;