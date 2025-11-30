using EntityLayer.WebApplication.ViewModels.ServiceViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IValidator<ServiceAddVM> _addValidation;
        private readonly IValidator<ServiceUpdateVM> _updateValidation;

        public ServiceController(IServiceService serviceService, IValidator<ServiceAddVM> addValidation, 
            IValidator<ServiceUpdateVM> updateValidation)
        {
            _serviceService = serviceService;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
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
            var validation = await _addValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _serviceService.AddServiceAsync(request);

                return RedirectToAction("GetAllServiceList", "Service", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
            var validation = await _updateValidation.ValidateAsync(request);

            if (validation.IsValid)
            {
                await _serviceService.UpdateServiceAsync(request);

                return RedirectToAction("GetAllServiceList", "Service", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
