using EntityLayer.WebApplication.ViewModels.ContactViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    #region Why we use area here
    // Admin Area: Used to organize all admin-related pages.
    // Each Area contains its own Controllers, Views, and ViewModels
    // to keep the project structure clean and modular.
    //     /Admin/{controller}/{action}/{id?}
    // Without this attribute, the controller will NOT be matched
    // to the Admin Area even if it is inside the Admin folder.
    #endregion

    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService; // take reference from ContactService

        public ContactController(IContactService contactService)
        {
            _contactService = contactService; // inject contactService via constructor
        }




        public async Task<IActionResult> GetAllContactList()
        {
            var contactList = await _contactService.GetAllContactListAsync();

            return View(contactList);
        }

        [HttpGet]
        public IActionResult AddContact()
        {
            return View(); // Return empty form to add new Contact.
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactAddVM request)
        {
            await _contactService.AddContactAsync(request); // Add new Contact to DB.

            return RedirectToAction("GetAllContactList", "Contact", new { Area = ("Admin") }); // Action + Controller + Area Name
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);

            return View(contact); // return Empty View with old information
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(ContactUpdateVM request)
        {
            await _contactService.UpdateContactAsync(request);

            return RedirectToAction("GetAllContactList", "Contact", new { Area = ("Admin") }); // Action + Controller + Area Name

        }

        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactConfirmed(int id)
        {
            await _contactService.DeleteContactAsync(id);

            return RedirectToAction("GetAllContactList", "Contact", new { Area = ("Admin") }); // Action + Controller + Area Name
        }
    }
}