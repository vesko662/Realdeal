using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class ArchiveController : Controller
    {
        public IActionResult Add(string advertId)
        {
            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Upload(string advertId)
        {
            return RedirectToAction(nameof(AdvertController),nameof(AdvertController.All));
        }
    }
}
