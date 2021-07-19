using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Data.Models
{
    public class MainCategory
    {

        public MainCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SubCategories = new HashSet<SubCategory>();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.ModifiedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(categoryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }


        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
