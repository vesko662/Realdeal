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

        public Data.Models.Message CreateMessage(string senderId, string recieverId, string advertId, string content)
        {
            var advers = context.Adverts.ToList();
            if (!advers.Any(x => x.Id == advertId))
            {
                return null;
            }

            var message = new Data.Models.Message
            {
                SenderId = senderId,
                ReciverId = recieverId,
                AdvertId = advertId,
                Content = content,
            };

            context.Messages.Add(message);
            context.SaveChanges();

            return message;
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
                    SenderId=lastMsg.SenderId,
                    Resiever = userService.GetUsernameById(lastMsg.ReciverId),
                    ResieverId=lastMsg.ReciverId,
                    LastMessageSender=userService.GetUserFullName(lastMsg.SenderId),
                };

                inboxMessages.Add(inboxMessage);
            }

            return inboxMessages;
        }

        public IEnumerable<MessageViewModel> GetMessagesForAdvert(string advertId, string senderId, string recieverId)
        {
            var advert = context.Adverts.Find(advertId);
            var advertOwnerId = advert.UserId;

            var messages = context.Messages
                .Where(x => x.AdvertId == advertId && (x.SenderId == senderId || x.SenderId == advertOwnerId || x.ReciverId == senderId) &&
                (x.ReciverId == recieverId || x.ReciverId == advertOwnerId || x.SenderId == recieverId))
                .OrderBy(date => date.CreatedOn)
                .Select(s => new MessageViewModel
                {
                    AdvertId = advertId,
                    Content = s.Content,
                    CreatedOn = s.CreatedOn.ToString("MM/dd hh:mm tt"),
                    RecieverId = recieverId,
                    SenderId = senderId,
                    SenderName = userService.GetUserFullName(s.SenderId)
                })
                .ToList();

           // messages
                //.ForEach(x => x.Content = HttpUtility.HtmlDecode(x.Content));

            return messages;
        }

        public MessageViewModel GetMessageViewModel(string advertId, string senderId, string recieverId)
        {
            var messageViewModel = new MessageViewModel()
            {
                SenderId = senderId,
                RecieverId = recieverId,
                AdvertId = advertId,
                SenderName = userService.GetUserFullName(senderId),
            };

            return messageViewModel;
        }
    }
}
