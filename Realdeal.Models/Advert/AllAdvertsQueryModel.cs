using Realdeal.Models.Advert.Enum;
using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Models.Advert
{
    public class AllAdvertsQueryModel
    {
        public string Search { get; set; }
        public string CategoryId { get; set; }
        public AdvertSorting AdvertSorting { get; set; }
        public IEnumerable<AdvertShowingViewModel> Advers { get; set; }
        public IEnumerable<CategoriesShowingViewModel> Categories { get; set; }
    }
}
