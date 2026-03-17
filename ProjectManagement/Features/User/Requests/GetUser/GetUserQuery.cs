using MediatR;

namespace ProjectManagement.Features.User.Requests.GetUser
{
    public record GetUserQuery(Guid Id) : IRequest<GetUserResult>;
}
