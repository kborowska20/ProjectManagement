using AutoMapper;
using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.GetProject
{
    public class GetProjectHandler : IRequestHandler<GetProjectQuery,GetProjectResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GetProjectResult> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _repositoryManager.Project.GetProjectByIdAsync(request.Id);

            if (project is null)
                throw new ArgumentNullException("proj not found");

            return new GetProjectResult(project.Id, project.ProjectName, project.Description, project.Status, project.Users, project.Tasks); ;
        }
    }
}
