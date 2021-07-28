using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Advert;
using Realdeal.Service.Advert;
using Realdeal.Service.Category;
using System.Security.Claims;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly ICategoryService categoryService;

        public AdvertController(IAdvertService advertService, ICategoryService categoryService)
        {
            this.advertService = advertService;
            this.categoryService = categoryService;
        }

        [Authorize]
        public IActionResult Create()
        => View(new AdvertFormModel { Categories = categoryService.GetAllCategories() });

        [HttpPost]
        [Authorize]
        public IActionResult Create(AdvertFormModel advert)
        {
            if (!ModelState.IsValid)
            {
                advert.Categories = categoryService.GetAllCategories();

                return View(advert);
            }

            if (!categoryService.DoesCategoryExist(advert.CategoryId))
            {
                this.ModelState.AddModelError(nameof(advert.CategoryId), "Category does not exist.");
            }

            advertService.CreateAdvert(advert, this.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var adverst=advertService

            return View();
        }

        public IActionResult Detail(string advertId)
        {

            return View();
        }

    }
}
