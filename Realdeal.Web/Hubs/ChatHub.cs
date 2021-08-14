using Microsoft.AspNetCore.SignalR;
using Realdeal.Service.Message;
using Realdeal.Service.User;
using System.Threading.Tasks;

namespace Realdeal.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService messagesService;
        private readonly IUserService userService;

        public ChatHub(
            IMessageService messagesService,
            IUserService userService)
        {
            this.messagesService = messagesService;
            this.userService = userService;
        }

        public async Task SendMessage(string advertId, string senderId, string recieverId, string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var message = messagesService.CreateMessage( senderId, recieverId, advertId, content);

                if (message==null)
                    return;

                var viewMessage = messagesService.GetMessageViewModel(advertId, senderId, recieverId);
                viewMessage.Content = message.Content;
                viewMessage.CreatedOn = message.CreatedOn.ToString("MM/dd hh:mm tt");


                await Clients.Users(recieverId, senderId).SendAsync("NewMessage", viewMessage);
            }
        }

    }
}
