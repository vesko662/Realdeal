using System;
using System.Collections.Generic;

namespace Realdeal.Models.Archive
{
    public class ArchiveAdvertDetailModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public DateTime CreatedOn { get; set; }

        public ArchiveAdvertStatisticsMode Statistics { get; set; }
    }
}
