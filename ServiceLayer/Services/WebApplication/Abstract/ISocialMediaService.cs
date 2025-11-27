using EntityLayer.WebApplication.ViewModels.SocialMediaViewModels;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaListVM>> GetAllSocialMediaListAsync();
        Task AddSocialMediaAsync(SocialMediaAddVM request);
        Task DeleteSocialMediaAsync(int id);
        Task<SocialMediaUpdateVM> GetSocialMediaByIdAsync(int id);
        Task UpdateSocialMediaAsync(SocialMediaUpdateVM request);
    }
}