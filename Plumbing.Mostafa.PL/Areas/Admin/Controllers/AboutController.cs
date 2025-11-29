using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.FluentValidation.WebApplication.AboutValidation;
using ServiceLayer.Services.WebApplication.Abstract;

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

    #region Validator

    /*
FluentValidation setup & flow (summary):

1) Validator per ViewModel:
   - Create a class: `class AboutAddValidation : AbstractValidator<AboutAddVM>`
   - Rules are defined in the constructor (they are registered, not executed there).

2) Dependency Injection:
   - Inject `IValidator<AboutAddVM>` (NOT IValidator<AboutAddValidation>).
   - Example: `public AboutController(IValidator<AboutAddVM> addValidator, ...)`

3) Auto vs Manual validation:
   - Auto: services.AddFluentValidationAutoValidation(opt => opt.DisableDataAnnotationsValidation = true);
           services.AddValidatorsFromAssemblyContaining<AboutAddValidation>();
           -> Validation runs automatically before the action; ModelState is populated.
   - Manual: var result = await _addValidator.ValidateAsync(request);
             if (!result.IsValid) result.AddToModelState(ModelState); // requires `using FluentValidation.AspNetCore;`

4) ModelState & UI behavior:
   - Returning `return View(request);` preserves user input in the form.
   - Validation messages show under fields via `<span asp-validation-for="Field"></span>` using ModelState errors.

5) Why DisableDataAnnotationsValidation = true?
   - To avoid duplicate/conflicting rules (we use FluentValidation only).
*/


    #endregion
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IValidator<AboutAddVM> _addValidator;
        private readonly IValidator<AboutUpdateVM> _updateValidator;

        public AboutController(IAboutService aboutService, IValidator<AboutAddVM>  addValidation,
            IValidator<AboutUpdateVM> updateValidation)
        {
            _aboutService = aboutService;
            _addValidator = addValidation;
            _updateValidator = updateValidation; 
        }





        public async Task<IActionResult> GetAllAboutList()
        {
            var aboutList = await _aboutService.GetAllAboutListAsync();

            return View(aboutList);
        }

        #region Add Actions
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View(); // Return empty form to add new about.
        }

        [HttpPost]
        public async Task<IActionResult> AddAbout(AboutAddVM request)
        {
            var validation = await _addValidator.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _aboutService.AddAboutAsync(request); // Add new about to DB.
                return RedirectToAction("GetAllAboutList", "About", new { Area = ("Admin") }); // Action + Controller + Area Name
            }

            validation.AddToModelState(this.ModelState);

            return View();
        }
        #endregion

        #region Update Actions
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await _aboutService.GetAboutByIdAsync(id);

            return View(about); // return Empty View with old information
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM request)
        {
            var validation = await _updateValidator.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _aboutService.UpdateAboutAsync(request);

                return RedirectToAction("GetAllAboutList", "About", new { Area = ("Admin") }); // Action + Controller + Area Name
            }

            validation.AddToModelState(this.ModelState);

            return View();
        }
        #endregion

        #region Delete Actions
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
        #endregion
    }
}
