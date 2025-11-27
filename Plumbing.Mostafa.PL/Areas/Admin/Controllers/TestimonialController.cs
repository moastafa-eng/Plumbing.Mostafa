using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
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
            await _testimonialService.AddTestimonialAsync(request);

            return RedirectToAction("GetAllTestimonialList", "Testimonial", new { area = ("Admin") });
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
            await _testimonialService.UpdateTestimonialAsync(request);

            return RedirectToAction("GetAllTestimonialList", "Testimonial", new { area = ("Admin") });
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
