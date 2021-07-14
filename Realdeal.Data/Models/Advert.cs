using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsАrchived { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<Message> Messages { get; set; }


        public ICollection<ОbservedAdvert> ОbservedAdverts { get; set; }

    }
}
