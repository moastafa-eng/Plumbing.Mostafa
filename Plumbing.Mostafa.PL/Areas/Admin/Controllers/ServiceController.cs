using EntityLayer.WebApplication.ViewModels.ServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }



        public async Task<IActionResult> GetAllServiceList()
        {
            var serviceList = await _serviceService.GetAllServiceListAsync();

            return View(serviceList);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceAddVM request)
        {
            await _serviceService.AddServiceAsync(request);

            return RedirectToAction("GetAllServiceList", "Service", new { area = ("Admin") });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceUpdateVM request)
        {
            await _serviceService.UpdateServiceAsync(request);

            return RedirectToAction("GetAllServiceList", "Service", new { area = ("Admin") });
        }

        [HttpGet]
        public IActionResult DeleteService(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteServiceConfirmed(int id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return RedirectToAction("GetAllServiceList", "Service", new { area = ("Admin") });
        }
    }
}
