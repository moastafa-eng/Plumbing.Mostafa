using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.SocialMediaViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace SocialMediaLayer.SocialMedias.Concrete
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<SocialMedia> _repository;
        public SocialMediaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<SocialMedia>();
        }




        public async Task<List<SocialMediaListVM>> GetAllSocialMediaListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var socialMediaList = await _unitOfWork.GetGenericRepository<SocialMedia>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to SocialMediaListVM
            //var socialMediaListVM = _mapper.Map<List<SocialMediaListVM>>(socialMediaList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to SocialMediaListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var socialMediaListVM = await _repository.GetAllEntityList()
                .ProjectTo<SocialMediaListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return socialMediaListVM;
        }

        public async Task AddSocialMediaAsync(SocialMediaAddVM request)
        {
            var socialMedia = _mapper.Map<SocialMedia>(request);

            await _repository.AddEntityAsync(socialMedia); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteSocialMediaAsync(int id)
        {
            var socialMedia = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(socialMedia);
            await _unitOfWork.CommitAsync();
        }

        public async Task<SocialMediaUpdateVM> GetSocialMediaByIdAsync(int id) // i Added Async after method name 
        {
            var socialMedia = await _repository.Where(x => x.Id == id).ProjectTo<SocialMediaUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return socialMedia;
        }

        public async Task UpdateSocialMediaAsync(SocialMediaUpdateVM request)
        {
            var socialMedia = _mapper.Map<SocialMedia>(request);

            _repository.UpdateEntity(socialMedia);
            await _unitOfWork.CommitAsync();
        }
    }
}
