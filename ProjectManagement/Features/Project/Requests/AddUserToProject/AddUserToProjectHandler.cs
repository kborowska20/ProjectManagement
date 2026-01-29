using AutoMapper;
using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public class AddUserToProjectHandler : IRequestHandler<AddUserCommand, AddUserResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddUserToProjectHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddUserResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var console = await _repositoryManager.Project.GetProjectByIdAsync(request.ProjectId);

            if (console is null)
                throw new ArgumentNullException("proj not found");


            //var user = new Domain.User()
            //{
            //    Name = request.Name,
            //    Publisher = request.Publisher,
            //    ConsoleId = request.ConsoleId
            //};

            //_repositoryManager.Project.AddGameToConsole(request.ConsoleId, game);

            //await _repositoryManager.SaveAsync();


            return null;
        }
    }
}
