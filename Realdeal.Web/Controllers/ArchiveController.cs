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

            archiveService.AddAdvertToArchive(advertId);

            return RedirectToAction(nameof(All));
        }
        public IActionResult All()
        {
            var archive = archiveService.GetArchiveAdverts();

            return View(archive);
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
