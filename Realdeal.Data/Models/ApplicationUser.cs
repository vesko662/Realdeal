using Microsoft.AspNetCore.Identity;
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
            this.ОbservedAdverts = new HashSet<ОbservedAdvert>();
            this.AdvertReports = new HashSet<AdvertReport>();
            this.Feedbacks = new HashSet<Feedback>();
            this.CreteOn = DateTime.UtcNow;
        }

        public string ProfilePhotoUrl { get; set; }

        [Required]
        [MaxLength(userDefaultMaxLenght)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(userDefaultMaxLenght)]
        public string Lastname { get; set; }

        [Required]
        public DateTime CreteOn { get; set; }

        public ICollection<Advert> Adverts { get; set; }

        [InverseProperty("User")]
        public ICollection<AdvertReport> AdvertReports { get; set; }
        public ICollection<ОbservedAdvert> ОbservedAdverts { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
