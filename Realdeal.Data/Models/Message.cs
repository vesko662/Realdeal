using System.ComponentModel.DataAnnotations;

namespace Realdeal.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }


        public string ReciverId { get; set; }

        public virtual ApplicationUser Reciver { get; set; }


        public string AdvertId { get; set; }

        public  Advert Advert { get; set; }
    }
}
