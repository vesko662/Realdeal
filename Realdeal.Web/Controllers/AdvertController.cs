using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Advert;
using Realdeal.Service.Advert;
using Realdeal.Service.Category;
using Realdeal.Service.User;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public AdvertController(IAdvertService advertService, ICategoryService categoryService,IUserService userService)
        {
            this.advertService = advertService;
            this.categoryService = categoryService;
            this.userService = userService;
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

            foreach (var image in advert.Images)
            {
                if (image.Length > 3 * 1024 * 1024)
                {
                    this.ModelState.AddModelError(nameof(advert.Images), "Maximum image size is 3 mb.");

                    advert.Categories = categoryService.GetAllCategories();

                    return View(advert);
                }
            }

            if (advert.Images.Count > 5)
            {
                this.ModelState.AddModelError(nameof(advert.Images), "Images can not be more than 5.");

                advert.Categories = categoryService.GetAllCategories();

                return View(advert);
            }

            if (!categoryService.DoesCategoryExist(advert.CategoryId))
            {
                this.ModelState.AddModelError(nameof(advert.CategoryId), "Category does not exist.");

                advert.Categories = categoryService.GetAllCategories();

                return View(advert);
            }

            advertService.CreateAdvert(advert);


            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllAdvertsQueryModel queryAdverts)
        {

            var adverst = advertService.GetAllAdverts(queryAdverts);

            adverst.Categories = categoryService.GetAllCategories();

            return View(adverst);
        }

        public IActionResult Detail(string advertId)
        {
            var advert = advertService.GetAdvertById(advertId);

            return View(advert);
        }

        [Authorize]
        public IActionResult Delete(string advertId)
        {
            bool isSuccessful = false;
           if(userService.IsUserAdmin() || userService.GetCurrentUserId() == userService.GetUserIdByAdvertId(advertId))
            {
                isSuccessful= advertService.DeleteAdvert(advertId);
            }

            if (isSuccessful)
            {
              return  RedirectToAction("MyAdverts", "User");
            }
#warning i dont know is it working
            return RedirectToAction("Error","Home");
        }

    }
}
