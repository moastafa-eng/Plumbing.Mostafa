using EntityLayer.WebApplication.ViewModels.ContactViewModels;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IContactService
    {
        Task<List<ContactListVM>> GetAllContactListAsync();
        Task AddContactAsync(ContactAddVM request);
        Task DeleteContactAsync(int id);
        Task<ContactUpdateVM> GetContactByIdAsync(int id);
        Task UpdateContactAsync(ContactUpdateVM request);
    }
}
