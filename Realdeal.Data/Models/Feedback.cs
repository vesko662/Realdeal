using System;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Data.Models
{
    public class Feedback
    {
        public Feedback()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDone = false;
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(reportDescriptionMaxLenght)]
        public string Description { get; set; }

        public string MakerId { get; set; }
        public ApplicationUser Maker { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
