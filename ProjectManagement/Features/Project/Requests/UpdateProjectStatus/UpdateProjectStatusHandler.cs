using AutoMapper;
using MediatR;
using ProjectManagement.Domain;
using ProjectManagement.Features.TaskItem.Requests.CreateTask;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public class UpdateProjectStatusHandler : IRequestHandler<UpdateProjectStatusCommand>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateProjectStatusHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _repositoryManager.Project.GetProjectByIdAsync(request.ProjectId);

            if (project is null)
                throw new ArgumentNullException("task not found");

            var status = await _repositoryManager.Project.UpdateProjectStatus(request.ProjectId, request.StatusId);

            await _repositoryManager.SaveAsync();
            _mapper.Map(request, status);

            return;
        }
    }
}
