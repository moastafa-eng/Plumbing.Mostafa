using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class TestimonialMapper : Profile
    {
        public TestimonialMapper()
        {
            CreateMap<Testimonial, TestimonialListVM>().ReverseMap();
            CreateMap<Testimonial, TestimonialAddVM>().ReverseMap();
            CreateMap<Testimonial, TestimonialUpdateVM>().ReverseMap();
        }
    }
}
