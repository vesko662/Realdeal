using System.Collections.Generic;

namespace Realdeal.Models.Message
{
    public class MessageChatViewModel
    {
        public string AdvertId { get; set; }
        public string AdvertName { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string RecieverName { get; set; }
        public SendMessageInputModel InputModel { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
