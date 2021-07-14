using System;
using System.ComponentModel.DataAnnotations;


namespace Realdeal.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Description{ get; set; }

        public string CreatorId { get; set; }
        public  ApplicationUser Creator { get; set; }


        public string CommentedId { get; set; }
        public ApplicationUser Commented { get; set; }

        [Required]
        public DateTime CreatedOn { get; private set; }

        [Required]
        public bool IsDeleted { get; }
    }
}
