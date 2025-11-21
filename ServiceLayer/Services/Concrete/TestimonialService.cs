using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Testimonial> _repository;
        public TestimonialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Testimonial>();
        }




        public async Task<List<TestimonialListVM>> GetAllTestimonialListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var testimonialList = await _unitOfWork.GetGenericRepository<Testimonial>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to TestimonialListVM
            //var testimonialListVM = _mapper.Map<List<TestimonialListVM>>(testimonialList); 
            #endregion

            // Translate Mapping to Query in data base and returns just required Field to TestimonialListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var testimonialListVM = await _repository.GetAllEntityList()
                .ProjectTo<TestimonialListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return testimonialListVM;
        }

        public async Task AddTestimonialAsync(TestimonialAddVM request)
        {
            var testimonial = _mapper.Map<Testimonial>(request);

            await _repository.AddEntityAsync(testimonial); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(testimonial);
            await _unitOfWork.CommitAsync();
        }

        public async Task<TestimonialUpdateVM> GetTestimonialByIdAsync(int id) // i Added Async after method name 
        {
            var testimonial = await _repository.Where(x => x.Id == id).ProjectTo<TestimonialUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return testimonial;
        }

        public async Task UpdateTestimonialAsync(TestimonialUpdateVM request)
        {
            var testimonial = _mapper.Map<Testimonial>(request);

            _repository.UpdateEntity(testimonial);
            await _unitOfWork.CommitAsync();
        }
    }
}
