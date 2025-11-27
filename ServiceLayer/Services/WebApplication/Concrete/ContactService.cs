using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactViewModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Contact> _repository;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Contact>();
        }




        public async Task<List<ContactListVM>> GetAllContactListAsync()
        {
            #region Bad Practice For Mapping
            // Get All Fields from Data base and Stored them in memory;
            //var contactList = await _unitOfWork.GetGenericRepository<Contact>().GetAllEntityList().ToListAsync();

            // Mapping only Required fields from memory to ContactListVM
            //var contactListVM = _mapper.Map<List<ContactListVM>>(contactList); 
            #endregion


            // Translate Mapping to Query in data base and returns just required Field to ContactListVM
            // this is way for mapping is faster than the previous way and good in Performance
            var contactListVM = await _repository.GetAllEntityList()
                .ProjectTo<ContactListVM>(_mapper.ConfigurationProvider).ToListAsync();

            return contactListVM;
        }

        public async Task AddContactAsync(ContactAddVM request)
        {
            var contact = _mapper.Map<Contact>(request);

            await _repository.AddEntityAsync(contact); // _context tracking the obj to add.
            await _unitOfWork.CommitAsync(); // using await with async and task did not BLOCK the thread operation while adding a new entity.
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _repository.GetEntityByIdAsync(id);

            _repository.DeleteEntity(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ContactUpdateVM> GetContactByIdAsync(int id) // i Added Async after method name 
        {
            var contact = await _repository.Where(x => x.Id == id).ProjectTo<ContactUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return contact;
        }

        public async Task UpdateContactAsync(ContactUpdateVM request)
        {
            var contact = _mapper.Map<Contact>(request);

            _repository.UpdateEntity(contact);
            await _unitOfWork.CommitAsync();
        }
    }
}
