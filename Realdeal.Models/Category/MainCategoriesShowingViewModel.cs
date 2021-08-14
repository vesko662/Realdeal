using System.Collections.Generic;

namespace Realdeal.Models.Category
{
    public class MainCategoriesShowingViewModel
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public IEnumerable<CategoryModel> SubCategories { get; set; }
    }
}
