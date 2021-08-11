using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Advert;
using Realdeal.Service.Advert;
using Realdeal.Service.Category;
using Realdeal.Service.User;
using System.Collections.Generic;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public AdvertController(IAdvertService advertService, ICategoryService categoryService, IUserService userService)
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

            if (!categoryService.DoesCategoryExist(advert.CategoryId))
            {
                this.ModelState.AddModelError(nameof(advert.CategoryId), "Category does not exist.");

                advert.Categories = categoryService.GetAllCategories();

                return View(advert);
            }

            if (ValidateImages(advert.Images) != null)
            {
                this.ModelState.AddModelError(nameof(advert.Images), ValidateImages(advert.Images));
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
            var advert = advertService.GetAdvertDetailsById(advertId);

            if (advert==null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(advert);
        }

        [Authorize]
        public IActionResult UserAdverts()
        {
            return View();
        }

       [Authorize]
        public IActionResult Delete(string advertId)
        {
            if (userService.IsUserAdmin() || userService.GetCurrentUserId() == userService.GetUserIdByAdvertId(advertId))
            {

                if (advertService.DeleteAdvert(advertId))
                {
                    return RedirectToAction(nameof(UserAdverts));
                }

                return RedirectToAction(nameof(HomeController.Error), nameof(HomeController));
            }

            return Unauthorized();
        }

        [Authorize]
        public IActionResult Edit(string advertId)
        {
            if (userService.IsUserAdmin() || userService.GetCurrentUserId() == userService.GetUserIdByAdvertId(advertId))
            {
                var advert = advertService.FindAdvertToEdit(advertId);
                advert.Categories = categoryService.GetAllCategories();
                return View(advert);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(string id, AdvertEditFormModel advert)
        {
            if (userService.IsUserAdmin() || userService.GetCurrentUserId() == userService.GetUserIdByAdvertId(id))
            {
                if (!ModelState.IsValid)
                {
                    advert.Categories = categoryService.GetAllCategories();

                    return View(advert);
                }

                if (!categoryService.DoesCategoryExist(advert.CategoryId))
                {
                    this.ModelState.AddModelError(nameof(advert.CategoryId), "Category does not exist.");

                    advert.Categories = categoryService.GetAllCategories();

                    return View(advert);
                }

                if (advert.Images != null)
                {
                    if (ValidateImages(advert.Images) != null)
                    {
                        this.ModelState.AddModelError(nameof(advert.Images), ValidateImages(advert.Images));
                        advert.Categories = categoryService.GetAllCategories();
                        return View(advert);
                    }
                }

                if (!advertService.EditAdvert(advert))
                {
                    return RedirectToAction(nameof(HomeController.Error), nameof(HomeController));
                }

                return RedirectToAction(nameof(All));
            }

            return Unauthorized();
        }

        private string ValidateImages(List<IFormFile> images)
        {
            foreach (var image in images)
            {
                if (image.Length > 3 * 1024 * 1024)
                {

                    return "Maximum image size is 3 mb.";
                }
            }

            if (images.Count > 5)
            {
                return "Images can not be more than 5.";
            }

            return null;
        }
    }
}
