using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Service.Category
{
    public interface ICategoryService
    {
        Dictionary<string, IEnumerable<CategoryModel>> GetAllCategories();

        bool DoesCategoryExist(string categoryId);
    }
}
