using System;

namespace Realdeal.Data.Models
{
    public class Advert
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
