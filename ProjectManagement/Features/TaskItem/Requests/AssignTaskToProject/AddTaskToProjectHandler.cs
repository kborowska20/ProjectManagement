using AutoMapper;
using MediatR;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.TaskItem.Request.AssignTaskToProject;
using ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject;
using ProjectManagement.Features.User.Repository;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests
{
    public class AddTaskToProjectHandler : IRequestHandler<AddProjectCommand, AddProjectResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddTaskToProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddProjectResult> Handle(AddProjectCommand request, CancellationToken cancellationToken)
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

            var result = _mapper.Map<AddProjectResult>(usersProjectTask);

            return result;
        }
    }
}
