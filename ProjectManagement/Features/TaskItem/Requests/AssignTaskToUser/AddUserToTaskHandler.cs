using AutoMapper;
using MediatR;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.User.Repository;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public class AddTaskToProjectHandler : IRequestHandler<AddTaskToUserCommand, AddProjectResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddTaskToProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddProjectResult> Handle(AddTaskToUserCommand request, CancellationToken cancellationToken)
        {
            var task = await _repositoryManager.TaskItem.GetTaskByIdAsync(request.TaskId);

            if (task  is null)
                throw new ArgumentNullException("task not found");
 
            var user = await _repositoryManager.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentNullException("user not found");

            var usersProjectTask = new UsersProjectTask()
            {
                ProjectId = null,
                TaskId = request.TaskId,
                UserId = request.UserId
            };
            _repositoryManager.TaskItem.AssignTaskToUser(usersProjectTask);
             
            await _repositoryManager.SaveAsync();

            var result = _mapper.Map<AddProjectResult>(usersProjectTask);

            return result;
        }
    }
}
