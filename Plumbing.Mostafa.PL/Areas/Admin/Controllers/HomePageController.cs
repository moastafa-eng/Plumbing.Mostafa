using EntityLayer.WebApplication.ViewModels.HomePageViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }




        public async Task<IActionResult> GetAllHomePageList()
        {
            var homePageList = await _homePageService.GetAllHomePageListAsync();

            return View(homePageList);
        }

        [HttpGet]
        public IActionResult AddHomePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHomePage(HomePageAddVM request)
        {
            await _homePageService.AddHomePageAsync(request);

            return RedirectToAction("GetAllHomePageList", "HomePage", new { area = ("Admin") });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHomePage(int id)
        {
            var homePage = await _homePageService.GetHomePageByIdAsync(id);

            return View(homePage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomePage(HomePageUpdateVM request)
        {
            await _homePageService.UpdateHomePageAsync(request);

            return RedirectToAction("GetAllHomePageList", "HomePage", new { area = ("Admin") });
        }

        [HttpGet]
        public IActionResult DeleteHomePage(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHomePageConfirmed(int id)
        {
            await _homePageService.DeleteHomePageAsync(id);

            return RedirectToAction("GetAllHomePageList", "HomePage", new { area = ("Admin") });
        }
    }
}
