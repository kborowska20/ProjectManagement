using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Requests.GetUser
{
    public record GetUserResult(
        Guid Id,
        string Name,
        string Email,
        string Role
    );
}
