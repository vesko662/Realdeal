using Microsoft.AspNetCore.Mvc;
using Realdeal.Web.Models.Advert;

namespace Realdeal.Web.Controllers
{
    public class AdvertController : Controller
    {
        public IActionResult Create()
        => View();

        [HttpPost]
        public IActionResult Create(AdvertFormModel advert)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
