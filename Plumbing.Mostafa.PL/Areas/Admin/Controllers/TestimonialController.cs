using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IValidator<TestimonialAddVM> _addValidation;
        private readonly IValidator<TestimonialUpdateVM> _updateValidation;

        public TestimonialController(ITestimonialService testimonialService, IValidator<TestimonialAddVM> addValidation, 
            IValidator<TestimonialUpdateVM> updateValidation)
        {
            _testimonialService = testimonialService;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
        }




        public async Task<IActionResult> GetAllTestimonialList()
        {
            var testimonialList = await _testimonialService.GetAllTestimonialListAsync();

            return View(testimonialList);
        }

        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(TestimonialAddVM request)
        {
            var validation = await _addValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _testimonialService.AddTestimonialAsync(request);

                return RedirectToAction("GetAllTestimonialList", "Testimonial", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var testimonial = await _testimonialService.GetTestimonialByIdAsync(id);

            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(TestimonialUpdateVM request)
        {
            var validation = await _updateValidation.ValidateAsync(request);

            if (validation.IsValid)
            {
                await _testimonialService.UpdateTestimonialAsync(request);

                return RedirectToAction("GetAllTestimonialList", "Testimonial", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
        }

        [HttpGet]
        public IActionResult DeleteTestimonial(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTestimonialConfirmed(int id)
        {
            await _testimonialService.DeleteTestimonialAsync(id);

            return RedirectToAction("GetAllTestimonialList", "Testimonial", new { area = ("Admin") });
        }
    }
}
