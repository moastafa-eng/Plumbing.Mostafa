using EntityLayer.WebApplication.ViewModels.CategoryViewModels;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListVM>> GetAllCategoryListAsync();
        Task AddCategoryAsync(CategoryAddVM request);
        Task DeleteCategoryAsync(int id);
        Task<CategoryUpdateVM> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryUpdateVM request);
    }
}
