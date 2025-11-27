using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Category> _repository;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Category>();
        }




        public async Task<List<CategoryListVM>> GetAllCategoryListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var categoryList = await _unitOfWork.GetGenericRepository<Category>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to CategoryListVM
            //var categoryListVM = _mapper.Map<List<CategoryListVM>>(categoryList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to CategoryListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var categoryListVM = await _repository.GetAllEntityList()
                .ProjectTo<CategoryListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return categoryListVM;
        }

        public async Task AddCategoryAsync(CategoryAddVM request)
        {
            var category = _mapper.Map<Category>(request);

            await _repository.AddEntityAsync(category); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CategoryUpdateVM> GetCategoryByIdAsync(int id) // i Added Async after method name 
        {
            var category = await _repository.Where(x => x.Id == id).ProjectTo<CategoryUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return category;
        }

        public async Task UpdateCategoryAsync(CategoryUpdateVM request)
        {
            var category = _mapper.Map<Category>(request);

            _repository.UpdateEntity(category);
            await _unitOfWork.CommitAsync();
        }
    }
}
