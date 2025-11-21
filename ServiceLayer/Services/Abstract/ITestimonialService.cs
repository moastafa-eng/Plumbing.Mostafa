using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;

namespace ServiceLayer.Services.Abstract
{
    public interface ITestimonialService
    {
        Task<List<TestimonialListVM>> GetAllTestimonialListAsync();
        Task AddTestimonialAsync(TestimonialAddVM request);
        Task DeleteTestimonialAsync(int id);
        Task<TestimonialUpdateVM> GetTestimonialByIdAsync(int id);
        Task UpdateTestimonialAsync(TestimonialUpdateVM request);
    }
}