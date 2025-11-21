using EntityLayer.WebApplication.ViewModels.TeamViewModels;

namespace ServiceLayer.Services.Abstract
{
    public interface ITeamService
    {
        Task<List<TeamListVM>> GetAllTeamListAsync();
        Task AddTeamAsync(TeamAddVM request);
        Task DeleteTeamAsync(int id);
        Task<TeamUpdateVM> GetTeamByIdAsync(int id);
        Task UpdateTeamAsync(TeamUpdateVM request);
    }
}