using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    #region Why we use area here
    // Admin Area: Used to organize all admin-related pages.
    // Each Area contains its own Controllers, Views, and ViewModels
    // to keep the project structure clean and modular.
    //     /Admin/{controller}/{action}/{id?}
    // Without this attribute, the controller will NOT be matched
    // to the Admin Area even if it is inside the Admin folder.
    #endregion

    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }





        public async Task<IActionResult> GetAllAboutList()
        {
            var aboutList = await _aboutService.GetAllAboutListAsync();

            return View(aboutList);
        }

        [HttpGet]
        public IActionResult AddAbout()
        {
            return View(); // Return empty form to add new about.
        }

        [HttpPost]
        public async Task<IActionResult> AddAbout(AboutAddVM request)
        {
            await _aboutService.AddAboutAsync(request); // Add new about to DB.

            return RedirectToAction("GetAllAboutList", "About", new { Area = ("Admin") }); // Action + Controller + Area Name
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await _aboutService.GetAboutByIdAsync(id);

            return View(about); // return Empty View with old information
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM request)
        {
            await _aboutService.UpdateAboutAsync(request);

            return RedirectToAction("GetAllAboutList", "About", new { Area = ("Admin") }); // Action + Controller + Area Name

        }

        [HttpGet]
        public IActionResult DeleteAbout(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAboutConfirmed(int id)
        {
            await _aboutService.DeleteAboutAsync(id);

            return RedirectToAction("GetAllAboutList", "About", new { Area = ("Admin") }); // Action + Controller + Area Name
        }
    }
}
