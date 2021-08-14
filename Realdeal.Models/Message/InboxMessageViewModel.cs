using System;

namespace Realdeal.Models.Message
{
    public class InboxMessageViewModel
    {
        public string AdvertId { get; set; }
        public string AdvertName { get; set; }
        public string Sender { get; set; }
        public string SenderId { get; set; }
        public string Resiever { get; set; }
        public string ResieverId { get; set; }
        public string Content { get; set; }
        public string LastMessageSender { get; set; }
        public string LastMessageDate{ get; set; }
    }
}
