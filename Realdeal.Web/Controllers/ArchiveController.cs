using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Service.Archive;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class ArchiveController : Controller
    {
        private readonly IArchiveService archiveService;

        public ArchiveController(IArchiveService archiveService)
        {
            this.archiveService = archiveService;
        }

        public IActionResult AddInArchive(string advertId)
        {
            if (archiveService.IsArchiveFull())
            {
                return RedirectToAction(nameof(All));
            }

            if (archiveService.AddAdvertToArchive(advertId))
            {
                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(ErrorController.Error), "Error");

        }
        public IActionResult All()
        {
            var archive = archiveService.GetArchiveAdverts();

            return View(archive);
        }
        public IActionResult AllTimeStatistics(string advertId)
        {
            var advert = archiveService.GetArchivedAdvert(advertId);

            if (advert == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(advert);
        }
        public IActionResult Upload(string advertId)
        {
            if (archiveService.UploadAdvert(advertId))
            {
                return RedirectToAction(nameof(AdvertController.All), "Advert");
            }

            return RedirectToAction(nameof(ErrorController.Error), "Error");
        }
    }
}
