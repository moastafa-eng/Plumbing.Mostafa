using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TeamViewModels;

namespace ServiceLayer.AutoMapper
{
    public class TeamMapper : Profile
    {
        public TeamMapper()
        {
            CreateMap<Team, TeamListVM>().ReverseMap();
            CreateMap<Team, TeamAddVM>().ReverseMap();
            CreateMap<Team, TeamUpdateVM>().ReverseMap();
        }
    }
}
