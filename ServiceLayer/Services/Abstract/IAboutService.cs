using EntityLayer.WebApplication.ViewModels.AboutViewModels;

namespace ServiceLayer.Services.Abstract
{
    public interface IAboutService
    {
        Task<List<AboutListVM>> GetAllAboutListAsync();
        Task AddAboutAsync(AboutAddVM request);
        Task DeleteAboutAsync(int id);
        Task<AboutUpdateVM> GetAboutByIdAsync(int id);
        Task UpdateAboutAsync(AboutUpdateVM request);
    }
}
