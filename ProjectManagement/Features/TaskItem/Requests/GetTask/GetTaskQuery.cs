using MediatR;

namespace ProjectManagement.Features.TaskItem.Requests.GetTask
{
    public record GetTaskQuery(Guid Id) : IRequest<GetTaskResult>;
}
