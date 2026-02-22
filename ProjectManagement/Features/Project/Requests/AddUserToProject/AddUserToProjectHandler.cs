using AutoMapper;
using MediatR;
using ProjectManagement.Domain;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public class AddUserToProjectHandler : IRequestHandler<AddUserToProjectCommand, AddUserToProjectResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddUserToProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddUserToProjectResult> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repositoryManager.Project.GetProjectByIdAsync(request.ProjectId);

            if (project is null)
                throw new ArgumentNullException("project not found");

            var user = await _repositoryManager.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentNullException("user not found");

            var usersProjectTask = new UsersProjectTask()
            {
                ProjectId = request.ProjectId,
                TaskId = null,
                UserId = request.UserId
            };

            _repositoryManager.Project.AssignUserToProject(usersProjectTask);

            await _repositoryManager.SaveAsync();

            var result = _mapper.Map<AddUserToProjectResult>(usersProjectTask);

            return result;
        }
    }
}
