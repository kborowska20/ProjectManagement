using MediatR;

namespace ProjectManagement.Features.User.Requests.UpdateUserRole
{
    public record UpdateUserRoleCommand(
        Guid UserId,
        Guid UserRoleId
    ) : IRequest;
}