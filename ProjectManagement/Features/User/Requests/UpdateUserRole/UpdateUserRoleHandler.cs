using AutoMapper;
using MediatR;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.Features.User.Requests.UpdateUserRole
{
    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateUserRoleHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {

            var user = await _repositoryManager.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentNullException("user not found");

            var role = await _repositoryManager.User.UpdateUserRoleAsync(request.UserId, request.UserRoleId);

            await _repositoryManager.SaveAsync();

            return;
        }
    }
}
