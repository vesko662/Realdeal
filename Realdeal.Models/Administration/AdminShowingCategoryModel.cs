using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Models.Administration
{
    public class AdminShowingCategoryModel
    {
        public IEnumerable<CategoriesShowingViewModel> Categories { get; set; }

        public IEnumerable<CategoryModel> UnassignedSubCategories { get; set; }

    }
}
