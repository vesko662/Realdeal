using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Realdeal.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Adverts = new HashSet<Advert>();
            this.Comments = new HashSet<Comment>();
            this.ОbservedAdverts = new HashSet<ОbservedAdvert>();
              #warning TODO:
            // this.ProfilePhotoUrl = deafaultPhotoUrl
        }

        public string ProfilePhotoUrl { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Lastname { get; set; }

        public ICollection<Advert> Adverts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ОbservedAdvert> ОbservedAdverts { get; set; }


    }
}
