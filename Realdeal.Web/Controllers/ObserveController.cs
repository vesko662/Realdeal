﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Service.Observe;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class ObserveController : Controller
    {
        private readonly IObserveService observeService;

        public ObserveController(IObserveService observeService)
        {
            this.observeService = observeService;
        }
        public IActionResult StartObserving(string advertId)
        {
            if (observeService.IsAdvertObserved(advertId))
                return RedirectToAction(nameof(Observing));

            bool isSuccessful = observeService.StartObservingAdvert(advertId);

            if (!isSuccessful)
                return RedirectToAction(nameof(HomeController.Error), "Home");

            return RedirectToAction(nameof(Observing));
        }
        public IActionResult Observing()
        {
            return View(observeService.GetAllObservingAdverts());
        }
        public IActionResult StopObserving(string advertId)
        {

            return Redirect("/");
        }
    }
}
