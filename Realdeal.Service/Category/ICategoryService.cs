using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Service.Category
{
    public interface ICategoryService
    {
        bool DoesCategoryExist(string categoryId);

        IEnumerable<MainCategoriesShowingViewModel> GetAllCategories();

    }
}
