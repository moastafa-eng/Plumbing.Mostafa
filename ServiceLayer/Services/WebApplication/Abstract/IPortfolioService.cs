using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListVM>> GetAllPortfolioListAsync();
        Task AddPortfolioAsync(PortfolioAddVM request);
        Task DeletePortfolioAsync(int id);
        Task<PortfolioUpdateVM> GetPortfolioByIdAsync(int id);
        Task UpdatePortfolioAsync(PortfolioUpdateVM request);
    }
}