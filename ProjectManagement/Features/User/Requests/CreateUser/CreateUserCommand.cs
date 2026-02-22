using MediatR;

namespace ProjectManagement.Features.User.Requests.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string Email,
        Guid RoleId
    ) : IRequest<Domain.User>;
}