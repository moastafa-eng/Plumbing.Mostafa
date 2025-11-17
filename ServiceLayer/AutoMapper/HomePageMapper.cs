using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageViewModels;

namespace ServiceLayer.AutoMapper
{
    public class HomePageMapper : Profile
    {
        public HomePageMapper()
        {
            CreateMap<HomePage, HomePageListVM>().ReverseMap();
            CreateMap<HomePage, HomePageAddVM>().ReverseMap();
            CreateMap<HomePage, HomePageUpdateVM>().ReverseMap();
        }
    }
}
