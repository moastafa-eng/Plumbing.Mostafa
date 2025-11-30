using EntityLayer.WebApplication.ViewModels.HomePageViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;
        private readonly IValidator<HomePageAddVM> _addValidation;
        private readonly IValidator<HomePageUpdateVM> _updateValidation;

        public HomePageController(IHomePageService homePageService, IValidator<HomePageAddVM> addValidation, 
            IValidator<HomePageUpdateVM> updateValidation)
        {
            _homePageService = homePageService;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
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
            var validation = await _addValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _homePageService.AddHomePageAsync(request);

                return RedirectToAction("GetAllHomePageList", "HomePage", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
            var validation = await _updateValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _homePageService.UpdateHomePageAsync(request);

                return RedirectToAction("GetAllHomePageList", "HomePage", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
