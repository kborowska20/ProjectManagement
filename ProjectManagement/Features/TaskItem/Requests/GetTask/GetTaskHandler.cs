using AutoMapper;
using MediatR;
using ProjectManagement.Features.TaskItem.Requests.GetTask;
using ProjectManagement.ServiceManager;

namespace TaskManagement.Features.Task.Requests.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery, GetTaskResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetTaskHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GetTaskResult> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _repositoryManager.TaskItem.GetTaskByIdAsync(request.Id);

            if (task is null)
                throw new ArgumentNullException(nameof(task), "Task not found");

            return new GetTaskResult(
                task.Id,
                task.Title,
                task.Desc,
                task.Project?.Id, // Pass the Project Id (Guid?) instead of the Project object
                task.User?.Id     // Pass the User Id (Guid?) instead of the User object
            );
        }
    }
}
