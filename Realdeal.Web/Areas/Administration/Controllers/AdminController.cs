using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Category;
using Realdeal.Service.Category;

namespace Realdeal.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ICategoryService categoryService;

        public AdminController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            //total users,total adverts,reports,feedbacks
            return View();
        }

        public IActionResult CreateMainCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMainCategory([FromForm] CreateMainCategoryFormModel createMainCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(createMainCategory);
            }

            if (createMainCategory.CategoryImg.Length > 3 * 1024 * 1024)
            {
                this.ModelState.AddModelError(nameof(createMainCategory.CategoryImg), "Maximum image size is 3 mb.");
                return View(createMainCategory);
            }

            if (categoryService.IsMainCategoryNameTaken(createMainCategory.Name))
            {
                this.ModelState.AddModelError(nameof(createMainCategory.Name), $"Category whit name \"{createMainCategory.Name}\" already exist");
                return View(createMainCategory);
            }

            categoryService.CreateMainCategory(createMainCategory);

            return RedirectToAction(nameof(AllCategories));
        }

        [HttpPost]
        public IActionResult EditMainCategory(CategoryEditFormModel category)
        {
            if (ModelState.IsValid)
            {
                categoryService.EditMainCategory(category);
            }

            return RedirectToAction(nameof(AllCategories));
        }
        public IActionResult DeleteMainCategory(string categoryId)
        {
            categoryService.DeleteMainCategory(categoryId);

            return RedirectToAction(nameof(AllCategories));
        }

        public IActionResult CreateSubCategory()
        {
            var category = new CreateSubCategoryFormModel()
            {
                MainCategories = categoryService.GetMainCategories(),
            };
            return View(category);
        }
        [HttpPost]
        public IActionResult CreateSubCategory(CreateSubCategoryFormModel createSubCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(createSubCategory.MainCategories = categoryService.GetMainCategories());
            }

            if (categoryService.IsSubCategoryNameTaken(createSubCategory.Name))
            {
                this.ModelState.AddModelError(nameof(createSubCategory.Name), $"Category whit name \"{createSubCategory.Name}\" already exist");
                createSubCategory.MainCategories = categoryService.GetMainCategories();
                return View(createSubCategory);
            }

            categoryService.CreateSubCategory(createSubCategory);

            return RedirectToAction(nameof(AllCategories));
        }
        [HttpPost]
        public IActionResult EditSubCategory(CategoryEditFormModel category)
        {
            if (ModelState.IsValid)
            {
                categoryService.EditSubCategory(category);
            }

            return RedirectToAction(nameof(AllCategories));
        }
        public IActionResult DeleteSubCategory(string categoryId)
        {
            categoryService.DeleteSubCategory(categoryId);

            return RedirectToAction(nameof(AllCategories));
        }

        public IActionResult AllCategories()
        {
            var categories = new AdminShowingCategoryModel()
            {
                Categories = categoryService.GetAllCategories(),
            };
            return View(categories);
        }

        public IActionResult Reports()
        {
            return View();
        }
        public IActionResult ReportIsDone(string reportId)
        {
            return RedirectToAction(nameof(Reports));
        }

        public IActionResult Feedbacks()
        {
            return View();
        }
        public IActionResult FeedbackIsDone(string feedbackId)
        {
            return RedirectToAction(nameof(Feedbacks));
        }
    }
}

