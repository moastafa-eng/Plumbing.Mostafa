using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class UserEditMapper : Profile
    {
        public UserEditMapper()
        {
            CreateMap<AppUser, UserEditVM>().ReverseMap();
        }
    }
}
