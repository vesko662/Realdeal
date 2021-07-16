using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Realdeal.Data.Models
{
    public class MainCategory
    {

        public MainCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SubCategories = new HashSet<SubCategory>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
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
