using EntityLayer.WebApplication.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService; // take reference from CategoryService

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; // inject categoryService via constructor
        }



        public async Task<IActionResult> GetAllCategoryList()
        {
            var categoryList = await _categoryService.GetAllCategoryListAsync();

            return View(categoryList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View(); // Return empty form to add new Category.
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddVM request)
        {
            await _categoryService.AddCategoryAsync(request); // Add new Category to DB.

            return RedirectToAction("GetAllCategoryList", "Category", new { Area = ("Admin") }); // Action + Controller + Area Name
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category); // return Empty View with old information
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateVM request)
        {
            await _categoryService.UpdateCategoryAsync(request);

            return RedirectToAction("GetAllCategoryList", "Category", new { Area = ("Admin") }); // Action + Controller + Area Name

        }

        [HttpGet]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategoryConfirmed([FromForm] int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction("GetAllCategoryList", "Category", new { Area = ("Admin") }); // Action + Controller + Area Name
        }
    }
}
