using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Realdeal.Models.Category;
using Realdeal.Models.Home;
using Realdeal.Models;
using Realdeal.Service.Advert;
using Realdeal.Service.Category;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Realdeal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAdvertService advertService;
        private readonly IMemoryCache memoryCache;

        public HomeController(ICategoryService categoryService,
            IAdvertService advertService,
            IMemoryCache memoryCache
            )
        {
            this.categoryService = categoryService;
            this.advertService = advertService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {

            var categoryCacheKey ="CategoryCacheKey";
            var categories = this.memoryCache.Get<IEnumerable<MainCategoriesShowingViewModel>>(categoryCacheKey);

            if (categories==null)
            {
                categories = categoryService.GetAllCategories();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                this.memoryCache.Set(categoryCacheKey, categories, cacheOptions);
            }

            var newestAdvert = advertService.GetNewestAdverts();

            var homePageModel = new HomeViewModel()
            {
                Categories = categories,
                NewestAdverts = newestAdvert,
            };

            return View(homePageModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
