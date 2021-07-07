
namespace Realdeal.Data.Models
{
    public class Message
    {
        public string Content { get; set; }

        public bool IsRead { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public string ReciverId { get; set; }

        public virtual ApplicationUser Reciver { get; set; }

        public string AdvertId { get; set; }

        public  Advert Advert { get; set; }
    }
}
