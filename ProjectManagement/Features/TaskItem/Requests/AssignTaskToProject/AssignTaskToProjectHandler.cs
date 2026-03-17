using AutoMapper;
using MediatR;
using ProjectManagement.Domain;
using ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests
{
    public class AssignTaskToProjectHandler : IRequestHandler<AssignTaskToProjectCommand, AssignTaskToProjectResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AssignTaskToProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AssignTaskToProjectResult> Handle(AssignTaskToProjectCommand request, CancellationToken cancellationToken)
        {
            var task = await _repositoryManager.TaskItem.GetTaskByIdAsync(request.TaskId);

            if (task  is null)
                throw new ArgumentNullException("task not found");
 
            var proj = await _repositoryManager.Project.GetProjectByIdAsync(request.ProjectId);

            if (proj is null)
                throw new ArgumentNullException("proj not found");

            var usersProjectTask = new UsersProjectTask()
            {
                ProjectId = request.ProjectId,
                TaskId = request.TaskId,
                UserId = null
            };
            _repositoryManager.TaskItem.AssignTaskToUser(usersProjectTask);
             
            await _repositoryManager.SaveAsync();

            var result = _mapper.Map<AssignTaskToProjectResult>(usersProjectTask);

            return result;
        }
    }
}
