using Realdeal.Data;
using Realdeal.Models.Message;
using Realdeal.Service.User;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly IUserService userService;
        private readonly RealdealDbContext context;

        public MessageService(IUserService userService,RealdealDbContext context)
        {
            this.userService = userService;
            this.context = context;
        }
        public IEnumerable<InboxMessageViewModel> GetInboxMessages()
        {
            var inboxMessages = new List<InboxMessageViewModel>();

            var userId = userService.GetCurrentUserId();

            var adverts = context.Adverts
                .Where(x=>x.IsАrchived==false && x.IsDeleted==false)
                .Where(x => x.Messages.Any(x => x.SenderId == userId || x.ReciverId == userId))
                .ToList();

            foreach (var advert in adverts.OrderByDescending(x=>x.CreatedOn))
            {
                var lastMsg = this.context.Adverts
               .Where(x => x.Id == advert.Id)
               .Select(x => x.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault())
               .FirstOrDefault();

                var inboxMessage = new InboxMessageViewModel()
                {
                    AdvertId = advert.Id,
                    AdvertName = advert.Name,
                    Content=lastMsg.Content,
                    LastMessageDate = lastMsg.CreatedOn.ToString("MM/dd hh:mm tt"),
                    Sender=userService.GetUsernameById(lastMsg.SenderId),
                    Resiever = userService.GetUsernameById(lastMsg.ReciverId),
                    LastMessageSender=userService.GetUserFullName(lastMsg.SenderId),
                };

                inboxMessages.Add(inboxMessage);
            }

            return inboxMessages;
        }
    }
}
