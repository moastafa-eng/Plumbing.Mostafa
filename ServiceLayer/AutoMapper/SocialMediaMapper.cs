using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.SocialMediaViewModels;

namespace ServiceLayer.AutoMapper
{
    public class SocialMediaMapper : Profile
    {
        public SocialMediaMapper()
        {
            CreateMap<SocialMedia, SocialMediaListVM>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaAddVM>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaUpdateVM>().ReverseMap();
        }
    }
}
