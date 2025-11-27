using EntityLayer.WebApplication.ViewModels.ServiceViewModels;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IServiceService
    {
        Task<List<ServiceListVM>> GetAllServiceListAsync();
        Task AddServiceAsync(ServiceAddVM request);
        Task DeleteServiceAsync(int id);
        Task<ServiceUpdateVM> GetServiceByIdAsync(int id);
        Task UpdateServiceAsync(ServiceUpdateVM request);
    }
}