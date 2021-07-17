﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Realdeal.Data.DataConstants;

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
        [MaxLength(userDefaultMaxLenght)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(userDefaultMaxLenght)]
        public string Lastname { get; set; }

        public ICollection<Advert> Adverts { get; set; }

        [InverseProperty("Commented")]
        public ICollection<Comment> Comments { get; set; }


        public ICollection<ОbservedAdvert> ОbservedAdverts { get; set; }


    }
}
