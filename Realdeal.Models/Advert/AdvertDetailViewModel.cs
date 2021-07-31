using System;
using System.Collections.Generic;

namespace Realdeal.Models.Advert
{
    public class AdvertDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public DateTime CreatedOn { get; set; }


        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string Username { get; set; }
        public DateTime UserSince { get; set; }

    }
}
