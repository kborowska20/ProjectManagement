using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.TaskItem.Requests.CreateTask
{
    public class CreateTaskItemHandler : IRequestHandler<CreateTaskItemCommand, Domain.TaskItem>
    {
        private readonly IRepositoryManager _repositoryManager;

        public CreateTaskItemHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Domain.TaskItem> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new Domain.TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                ProjectId = request.ProjectId,
                AssignedUserId = request.AssignedUserId
            };
            await _repositoryManager.TaskItem.CreateTaskAsync(taskItem);
            await _repositoryManager.SaveAsync();
            return taskItem;
        }
    }
}
