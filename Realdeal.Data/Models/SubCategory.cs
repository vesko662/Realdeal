using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Realdeal.Data.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Adverts = new HashSet<Advert>();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.ModifiedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get;  set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime ModifiedOn { get;  set; }

        public string MainCategoryId { get; set; }

        public  MainCategory MainCategory { get; set; }

        public ICollection<Advert> Adverts { get; set; }
    }
}
