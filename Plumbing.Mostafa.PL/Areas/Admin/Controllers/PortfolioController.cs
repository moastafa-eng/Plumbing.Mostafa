using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IValidator<PortfolioAddVM> _addValidation;
        private readonly IValidator<PortfolioUpdateVM> _updateValidation;

        public PortfolioController(IPortfolioService portfolioService, IValidator<PortfolioAddVM> addValidation,
            IValidator<PortfolioUpdateVM> updateValidation)
        {
            _portfolioService = portfolioService;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
        }




        public async Task<IActionResult> GetAllPortfolioList()
        {
            var portfolioList = await _portfolioService.GetAllPortfolioListAsync();

            return View(portfolioList);
        }

        [HttpGet]
        public IActionResult AddPortfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolio(PortfolioAddVM request) 
        {
            var validation = await _addValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _portfolioService.AddPortfolioAsync(request);

                return RedirectToAction("GetAllPortfolioList", "Portfolio", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePortfolio(int id)
        {
            var portfolio = await _portfolioService.GetPortfolioByIdAsync(id);

            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePortfolio(PortfolioUpdateVM request)
        {

            var validation = await _updateValidation.ValidateAsync(request);

            if (validation.IsValid)
            {
                await _portfolioService.UpdatePortfolioAsync(request);

                return RedirectToAction("GetAllPortfolioList", "Portfolio", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();

        }

        [HttpGet]
        public IActionResult DeletePortfolio(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePortfolioConfirmed(int id)
        {
            await _portfolioService.DeletePortfolioAsync(id);

            return RedirectToAction("GetAllPortfolioList", "Portfolio", new { area = ("Admin") });
        }
    }
}
