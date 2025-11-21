using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TeamViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Team> _repository;
        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Team>();
        }




        public async Task<List<TeamListVM>> GetAllTeamListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var teamList = await _unitOfWork.GetGenericRepository<Team>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to TeamListVM
            //var teamListVM = _mapper.Map<List<TeamListVM>>(teamList); 
            #endregion

            // Translate Mapping to Query in data base and returns just required Field to TeamListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var teamListVM = await _repository.GetAllEntityList()
                .ProjectTo<TeamListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return teamListVM;
        }

        public async Task AddTeamAsync(TeamAddVM request)
        {
            var team = _mapper.Map<Team>(request);

            await _repository.AddEntityAsync(team); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(team);
            await _unitOfWork.CommitAsync();
        }

        public async Task<TeamUpdateVM> GetTeamByIdAsync(int id) // i Added Async after method name 
        {
            var team = await _repository.Where(x => x.Id == id).ProjectTo<TeamUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return team;
        }

        public async Task UpdateTeamAsync(TeamUpdateVM request)
        {
            var team = _mapper.Map<Team>(request);

            _repository.UpdateEntity(team);
            await _unitOfWork.CommitAsync();
        }
    }
}