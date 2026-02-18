using AutoMapper;
using MediatR;
using ProjectManagement.Features.User.Repository;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests.AddUserToProject
{
    public class AddUserToTaskHandler : IRequestHandler<AddUserCommand, AddUserResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddUserToTaskHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddUserResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var task = await _repositoryManager.TaskItem.GetTaskByIdAsync(request.TaskId);

            if (task  is null)
                throw new ArgumentNullException("task not found");
 
            var user = await _repositoryManager.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentNullException("user not found");

            _repositoryManager.TaskItem.AssignTaskToUser(request.UserId, request.TaskId);
             
            await _repositoryManager.SaveAsync();

            return null;
        }
    }
}
