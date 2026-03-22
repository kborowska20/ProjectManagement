using AutoMapper;
using MediatR;
using ProjectManagement.Features.TaskItem.Requests.GetTask;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.User.Requests.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetUserHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repositoryManager.User.GetUserByIdAsync(request.Id);

            if (user is null)
                throw new ArgumentNullException(nameof(user), "User not found");

            return new GetUserResult(
                user.Id,
                user.Name,
                user.Email,
                user.UserRole // Fix: Pass RoleName (string) instead of UserRoleId (UserRole)
            );
        }
    }
}
