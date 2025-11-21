using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<HomePage> _repository;
        public HomePageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<HomePage>();
        }




        public async Task<List<HomePageListVM>> GetAllHomePageListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var homePageList = await _unitOfWork.GetGenericRepository<HomePage>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to HomePageListVM
            //var homePageListVM = _mapper.Map<List<HomePageListVM>>(homePageList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to HomePageListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var homePageListVM = await _repository.GetAllEntityList()
                .ProjectTo<HomePageListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return homePageListVM;
        }

        public async Task AddHomePageAsync(HomePageAddVM request)
        {
            var homePage = _mapper.Map<HomePage>(request);

            await _repository.AddEntityAsync(homePage); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(homePage);
            await _unitOfWork.CommitAsync();
        }

        public async Task<HomePageUpdateVM> GetHomePageByIdAsync(int id) // i Added Async after method name 
        {
            var homePage = await _repository.Where(x => x.Id == id).ProjectTo<HomePageUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return homePage;
        }

        public async Task UpdateHomePageAsync(HomePageUpdateVM request)
        {
            var homePage = _mapper.Map<HomePage>(request);

            _repository.UpdateEntity(homePage);
            await _unitOfWork.CommitAsync();
        }
    }
}
