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
            var user = new Domain.User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                UserRoleId = request.UserRoleId // Will be loaded/set separately based on RoleId
            };

            await _repositoryManager.User.CreateUserAsync(user);
            await _repositoryManager.SaveAsync();

            return user;
        }
    }
}
