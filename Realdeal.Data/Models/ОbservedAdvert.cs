using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realdeal.Data.Models
{
    public class ОbservedAdvert
    {
        public ОbservedAdvert()
        {
            this.StartedObserving = DateTime.UtcNow;
            SendEmailOnUpdate = false;
        }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string AdvertId { get; set; }
        public Advert Advert { get; set; }

        public DateTime StartedObserving { get; set; }

        [Required]
        public bool SendEmailOnUpdate { get; set; }
    }
}
