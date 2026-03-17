using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.UpdateProjectStatus
{
    public class UpdateProjectStatusHandler : IRequestHandler<UpdateProjectStatusCommand>
    {
        private readonly IRepositoryManager _repositoryManager;

        public UpdateProjectStatusHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _repositoryManager.Project.GetProjectByIdAsync(request.ProjectId);

            if (project is null)
                throw new KeyNotFoundException($"Project with ID {request.ProjectId} not found");

            await _repositoryManager.Project.UpdateProjectStatus(request.ProjectId, request.StatusId);
            await _repositoryManager.SaveAsync();
        }
    }
}
