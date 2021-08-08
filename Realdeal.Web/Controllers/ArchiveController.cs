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

        public IActionResult Add(string advertId)
        {
            archiveService.AddAdvertToArchive(advertId);

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
