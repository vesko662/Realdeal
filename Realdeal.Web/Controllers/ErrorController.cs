using Microsoft.AspNetCore.Mvc;

namespace Realdeal.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error/404")]
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
