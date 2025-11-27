using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
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
            await _portfolioService.AddPortfolioAsync(request);

            return RedirectToAction("GetAllPortfolioList", "Portfolio", new { area = ("Admin") });
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
            await _portfolioService.UpdatePortfolioAsync(request);

            return RedirectToAction("GetAllPortfolioList", "Portfolio", new { area = ("Admin") });
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
