using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    internal class SignInMapper : Profile
    {
        public SignInMapper()
        {
            CreateMap<AppUser, SignInVM>().ReverseMap();
        }
    }
}
