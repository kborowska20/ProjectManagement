using AutoMapper;
using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.GetProject
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery,GetTaskResult>
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
            var project = await _repositoryManager.Project.GetProjectByIdAsync(request.Id);

            if (project is null)
                throw new ArgumentNullException("proj not found");

            return new GetTaskResult(project.Id, project.ProjectName, project.Description, project.Status, project.Users, project.Tasks); ;
        }
    }
}
