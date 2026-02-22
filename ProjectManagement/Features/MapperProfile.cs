using AutoMapper;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.AddUserToProject;

namespace ProjectManagement.Features
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UsersProjectTask, AddUserToProjectResult>();
        }
    }
}
