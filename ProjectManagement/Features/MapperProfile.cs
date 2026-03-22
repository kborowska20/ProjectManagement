using AutoMapper;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.TaskItem.Requests.AddUserToProject;
using ProjectManagement.Features.TaskItem.Requests.AssignTaskToProject;

namespace ProjectManagement.Features
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AssignTaskToProjectResult, UsersProjectTask>()
                .ForMember(d => d.ProjectId, x => x.MapFrom(s => s.ProjectId))
                .ForMember(d => d.TaskId, x => x.MapFrom(s => s.TaskId))
                .ReverseMap();
            CreateMap<AddTaskToUserResult, UsersProjectTask>()
                .ForMember(d => d.UserId, x => x.MapFrom(s => s.UserId))
                .ForMember(d => d.TaskId, x => x.MapFrom(s => s.TaskId)).ReverseMap();
            CreateMap<AddUserToProjectResult, UsersProjectTask>()
                .ForMember(d => d.UserId, x => x.MapFrom(s => s.UserId))
                .ForMember(d => d.ProjectId, x => x.MapFrom(s => s.ProjectId)).ReverseMap();

        }
    }
}
