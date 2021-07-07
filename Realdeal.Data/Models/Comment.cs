﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string CommenterId { get; set; }
        public ApplicationUser Commenter { get; set; }

        [Required]
        public DateTime CreatedOn { get; private set; }

        [Required]
        public bool IsDeleted { get; }
    }
}
