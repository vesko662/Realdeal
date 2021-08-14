using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Service.Message;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public IActionResult Inbox()
        {
           var inbox= messageService.GetInboxMessages();

            return View(inbox);
        }
    }
}
