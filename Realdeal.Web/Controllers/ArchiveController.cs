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

            var isSuccessful = archiveService.AddAdvertToArchive(advertId);

            return RedirectToAction(nameof(All));
        }
        public IActionResult All()
        {
            var archive = archiveService.GetArchiveAdverts();

            return View(archive);
        }
        public IActionResult AllTimeStatistics(string advertId)
        {
            return View();
        }
        public IActionResult Upload(string advertId)
        {
           var isSuccessful = archiveService.UploadAdvert(advertId);

            if (!isSuccessful)
            {
                
            }

            return RedirectToAction(nameof(AdvertController),nameof(AdvertController.All));
        }
    }
}
