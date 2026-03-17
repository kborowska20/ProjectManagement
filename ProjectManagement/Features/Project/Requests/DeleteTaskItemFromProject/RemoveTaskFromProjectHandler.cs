using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;

public class RemoveTaskFromProjectHandler : IRequestHandler<RemoveTaskFromProjectCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly DataContext _context;

    public RemoveTaskFromProjectHandler(IRepositoryManager repositoryManager, DataContext context)
    {
        _repositoryManager = repositoryManager;
        _context = context;
    }

    public async Task Handle(RemoveTaskFromProjectCommand request, CancellationToken cancellationToken)
    {
        await _repositoryManager.Project.DeleteTaskFromProject(request.ProjectId, request.TaskId);
        await _repositoryManager.SaveAsync();
    }
}
