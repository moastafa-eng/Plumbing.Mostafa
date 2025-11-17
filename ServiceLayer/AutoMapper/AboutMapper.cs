using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutViewModels;

namespace ServiceLayer.AutoMapper
{
    public class AboutMapper : Profile
    {
        public AboutMapper()
        {
            CreateMap<About, AboutListVM>().ReverseMap();
            CreateMap<About, AboutAddVM>().ReverseMap();
            CreateMap<About, AboutUpdateVM>().ReverseMap();
        }
    }
}
