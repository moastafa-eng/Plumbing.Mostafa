using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Portfolio> _repository;
        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Portfolio>();
        }




        public async Task<List<PortfolioListVM>> GetAllPortfolioListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var portfolioList = await _unitOfWork.GetGenericRepository<Portfolio>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to PortfolioListVM
            //var portfolioListVM = _mapper.Map<List<PortfolioListVM>>(portfolioList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to PortfolioListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var portfolioListVM = await _repository.GetAllEntityList()
                .ProjectTo<PortfolioListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return portfolioListVM;
        }

        public async Task AddPortfolioAsync(PortfolioAddVM request)
        {
            var portfolio = _mapper.Map<Portfolio>(request);

            await _repository.AddEntityAsync(portfolio); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PortfolioUpdateVM> GetPortfolioByIdAsync(int id) // i Added Async after method name 
        {
            var portfolio = await _repository.Where(x => x.Id == id).ProjectTo<PortfolioUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return portfolio;
        }

        public async Task UpdatePortfolioAsync(PortfolioUpdateVM request)
        {
            var portfolio = _mapper.Map<Portfolio>(request);

            _repository.UpdateEntity(portfolio);
            await _unitOfWork.CommitAsync();
        }
    }
}
