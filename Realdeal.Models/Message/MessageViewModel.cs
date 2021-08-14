using System;

namespace Realdeal.Models.Message
{
    public class MessageViewModel
    {
        public string SenderName { get; set; }
        public string AdvertId { get; set; }
        public string Content { get; set; }
        public string CreatedOn { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
    }
}
