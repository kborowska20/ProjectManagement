using MediatR;
using ProjectManagement.Features.TaskItem.Requests.CreateTask;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, Domain.TaskItem>
    {
        private readonly IRepositoryManager _repositoryManager;

        public UpdateUserRoleHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Domain.TaskItem> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.TaskName,
                Desc = request.Description,
                User = null,
                Project = null // Will be set by EF Core based on navigation
            };

            await _repositoryManager.TaskItem.CreateTaskAsync(task);
            await _repositoryManager.SaveAsync();

            return task;
        }
    }
}
