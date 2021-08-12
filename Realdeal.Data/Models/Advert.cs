using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Data.Models
{
    public class Advert
    {
        public Advert()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.IsАrchived = false;
            this.Messages = new HashSet<Message>();
            this.ОbservedAdverts = new HashSet<ОbservedAdvert>();
            this.AdvertImages = new HashSet<AdvertImage>();
            this.AdvertReports = new HashSet<AdvertReport>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(advertNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(advertDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Viewed { get; set; }

        [Required]
        public bool IsАrchived { get; set; }

        public string SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<Message> Messages { get; set; }

        public ICollection<ОbservedAdvert> ОbservedAdverts { get; set; }

        public ICollection<AdvertImage> AdvertImages { get; set; }
        public ICollection<AdvertReport> AdvertReports { get; set; }

    }
}
