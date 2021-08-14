using System.Collections.Generic;
using Realdeal.Models.Message;

namespace Realdeal.Service.Message
{
    public interface IMessageService
    {
        public IEnumerable<InboxMessageViewModel> GetInboxMessages();
        public MessageViewModel GetMessageViewModel(string advertId, string senderId, string recieverId);
        public Data.Models.Message CreateMessage(string senderId, string recieverId, string advertId, string content);
        public IEnumerable<MessageViewModel> GetMessagesForAdvert(string advertId, string senderId, string recieverId);
        public void DeleteAllMessagesToAdvert(string advertId);

    }
}
