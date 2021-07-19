using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Web.Models.Advert;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        [Authorize]
        public IActionResult Create()
        => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(AdvertFormModel advert)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
