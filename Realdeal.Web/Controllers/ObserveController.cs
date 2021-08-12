using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realdeal.Web.Controllers
{
    public class ObserveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
