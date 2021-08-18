using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realdeal.Models.Message;
using Realdeal.Service.Advert;
using Realdeal.Service.Message;
using Realdeal.Service.User;

namespace Realdeal.Web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IAdvertService advertService;

        public MessageController(IMessageService messageService,
            IUserService userService,
            IAdvertService advertService)
        {
            this.messageService = messageService;
            this.userService = userService;
            this.advertService = advertService;
        }
        public IActionResult Inbox()
        {
           var inbox= messageService.GetInboxMessages();

            return View(inbox);
        }
        public IActionResult Chat(MessageChatViewModel chat)
        {
            if (chat.SenderId != userService.GetCurrentUserId() && chat.RecieverId != userService.GetCurrentUserId())
            {
                return this.Redirect("/");
            }

            var messages = messageService.GetMessagesForAdvert(chat.AdvertId, chat.SenderId, chat.RecieverId);

            chat.Messages = messages;

            chat.RecieverName = userService.GetUserFullName(chat.RecieverId);
            chat.AdvertName = advertService.GetAdvertName(chat.AdvertId);

            chat.InputModel = new SendMessageInputModel()
            {
                SenderId = chat.SenderId,
                AdvertId = chat.AdvertId,
                RecieverId = chat.RecieverId,
            };

            return View(chat);
        }
    }
}
