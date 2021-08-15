using Realdeal.Models.Advert;
using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<MainCategoriesShowingViewModel> Categories { get; set;}
        public IEnumerable<AdvertShowingViewModel> NewestAdverts { get; set;}
    }
    
}
