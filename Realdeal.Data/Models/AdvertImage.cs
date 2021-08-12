using System.ComponentModel.DataAnnotations;

namespace Realdeal.Data.Models
{
    public class AdvertImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}
