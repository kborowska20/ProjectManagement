using MediatR;

namespace ProjectManagement.Features.Project.Requests.GetProject
{
    public record GetProjectQuery(Guid Id) : IRequest<GetProjectResult>;
}
