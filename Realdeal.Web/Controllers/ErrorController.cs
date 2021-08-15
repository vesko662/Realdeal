using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
