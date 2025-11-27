using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Service> _repository;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Service>();
        }




        public async Task<List<ServiceListVM>> GetAllServiceListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var serviceList = await _unitOfWork.GetGenericRepository<Service>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to ServiceListVM
            //var serviceListVM = _mapper.Map<List<ServiceListVM>>(serviceList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to ServiceListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var serviceListVM = await _repository.GetAllEntityList()
                .ProjectTo<ServiceListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return serviceListVM;
        }

        public async Task AddServiceAsync(ServiceAddVM request)
        {
            var service = _mapper.Map<Service>(request);

            await _repository.AddEntityAsync(service); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceUpdateVM> GetServiceByIdAsync(int id) // i Added Async after method name 
        {
            var service = await _repository.Where(x => x.Id == id).ProjectTo<ServiceUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return service;
        }

        public async Task UpdateServiceAsync(ServiceUpdateVM request)
        {
            var service = _mapper.Map<Service>(request);

            _repository.UpdateEntity(service);
            await _unitOfWork.CommitAsync();
        }
    }
}
