using EntityLayer.WebApplication.ViewModels.HomePageViewModels;

namespace ServiceLayer.Services.Abstract
{
    public interface IHomePageService
    {
        Task<List<HomePageListVM>> GetAllHomePageListAsync();
        Task AddHomePageAsync(HomePageAddVM request);
        Task DeleteHomePageAsync(int id);
        Task<HomePageUpdateVM> GetHomePageByIdAsync(int id);
        Task UpdateHomePageAsync(HomePageUpdateVM request);
    }
}
