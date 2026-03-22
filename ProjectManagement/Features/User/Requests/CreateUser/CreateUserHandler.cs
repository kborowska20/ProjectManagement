using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.User.Requests.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Domain.User>
    {
        private readonly IRepositoryManager _repositoryManager;

        public CreateUserHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Domain.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _repositoryManager.User.CreateUserAsync(request);
            await _repositoryManager.SaveAsync();

            return user;
        }
    }
}
