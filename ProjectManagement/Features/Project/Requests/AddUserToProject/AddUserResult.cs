namespace ProjectManagement.Features.Project.Requests.AddUserToProject
{
    public record AddUserResult(Guid UserId, string Publisher, Guid ProjectId);
}
