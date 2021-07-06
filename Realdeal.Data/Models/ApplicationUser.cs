using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Realdeal.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Advert> Adverts { get; set; }
    }
}
