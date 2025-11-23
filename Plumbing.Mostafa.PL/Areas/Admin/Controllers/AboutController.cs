using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
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

            return RedirectToAction("GetAllAboutList", "AboutController", new { Area = ("Admin") }); // Action + Controller + Area Name
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

            return RedirectToAction("GetAllAboutList", "AboutController", new { Area = ("Admin") }); // Action + Controller + Area Name

        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutService.DeleteAboutAsync(id);

            return RedirectToAction("GetAllAboutList", "AboutController", new { Area = ("Admin") }); // Action + Controller + Area Name
        }
    }
}
