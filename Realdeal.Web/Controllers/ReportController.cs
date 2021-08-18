using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Report;
using Realdeal.Service.Report;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult AdvertReport(string advertId, string advertName)
        {
            return View(new AdvertReportFormModel { AdvertId = advertId, AdvertName = advertName });
        }

        [HttpPost]
        public IActionResult AdvertReport(AdvertReportFormModel advertReport)
        {
            if (!ModelState.IsValid)
            {
                return View(advertReport);
            }

            bool isSuccessful = reportService.ReportAdvert(advertReport);

            if (!isSuccessful)
            {
                return RedirectToAction(nameof(ErrorController.Error), "Error");
            }

            return Redirect("/");
        }

        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(FeedbackFormModel feedback)
        {
            if (!ModelState.IsValid)
            {
                return View(feedback);
            }

            reportService.CreateFeedback(feedback);

            return Redirect("/");
        }

    }
}
