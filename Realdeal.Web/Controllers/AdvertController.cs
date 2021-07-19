using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Web.Models.Advert;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        private readonly RealdealDbContext context;

        public AdvertController(RealdealDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Create()
        => View(new AdvertFormModel { Categories = GetAllCategories() });

        [HttpPost]
        [Authorize]
        public IActionResult Create(AdvertFormModel advert)
        {
            if (!ModelState.IsValid)
            {
                advert.Categories = this.GetAllCategories();

                return View(advert);
            }

            if (!this.context.SubCategories.Any(c => c.Id == advert.CategoryId))
            {
                this.ModelState.AddModelError(nameof(advert.CategoryId), "Category does not exist.");
            }



            var carData = new Advert
            {
                Name = advert.Name,
                Description = advert.Description,
                SubCategoryId = advert.CategoryId,
                ImgUrl = advert.ImgUrl,
                Price = advert.Price,
                UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };


            //this.data.Cars.Add(carData);
            //this.data.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        private Dictionary<string, IEnumerable<AdvertCategoryViewModel>> GetAllCategories()
        {
            return context.MainCategories
                .Select(s => new
                {
                    Name = s.Name,
                    SubCategories = s.SubCategories.Select(c => new AdvertCategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                })
                .ToDictionary(x => x.Name, s => s.SubCategories);
        }
    }
}
