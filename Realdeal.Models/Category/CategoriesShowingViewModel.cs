using System.Collections.Generic;

namespace Realdeal.Models.Category
{
    public class CategoriesShowingViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public IEnumerable<CategoryModel> SubCategories { get; set; }
    }
}
