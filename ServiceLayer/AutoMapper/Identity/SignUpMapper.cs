using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.VewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class SignUpMapper : Profile
    {
        public SignUpMapper()
        {
            CreateMap<AppUser, SignUpVM>().ReverseMap();
        }
    }
}
