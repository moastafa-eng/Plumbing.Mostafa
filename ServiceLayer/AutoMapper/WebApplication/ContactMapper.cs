using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactViewModels;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact, ContactListVM>().ReverseMap();
            CreateMap<Contact, ContactAddVM>().ReverseMap();
            CreateMap<Contact, ContactUpdateVM>().ReverseMap();
        }
    }
}
