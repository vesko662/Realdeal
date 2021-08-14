using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Models.Message
{
   public class SendMessageInputModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(messageMaxLenght)]
        public string Content { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string RecieverId { get; set; }

        [Required]
        public string AdvertId { get; set; }
    }
}
