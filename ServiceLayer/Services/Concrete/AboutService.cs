using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<About> _repository;
        public AboutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<About>();
        }




        public async Task<List<AboutListVM>> GetAllAboutListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var aboutList = await _unitOfWork.GetGenericRepository<About>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to aboutListVM
            //var aboutListVM = _mapper.Map<List<AboutListVM>>(aboutList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to aboutListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var aboutListVM = await _repository.GetAllEntityList()
                .ProjectTo<AboutListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return aboutListVM;
        }

        public async Task AddAboutAsync(AboutAddVM request)
        {
            var about = _mapper.Map<About>(request);

            await _repository.AddEntityAsync(about); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AboutUpdateVM> GetAboutByIdAsync(int id) // i Added Async after method name 
        {
            var about =  await _repository.Where(x => x.Id == id).ProjectTo<AboutUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return about;
        }

        public async Task UpdateAboutAsync(AboutUpdateVM request)
        {
            var about = _mapper.Map<About>(request);

            _repository.UpdateEntity(about);
            await _unitOfWork.CommitAsync();
        }
    }
}
