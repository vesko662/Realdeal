using Realdeal.Models.Advert;
using System.Collections.Generic;

namespace Realdeal.Service.Category
{
    public interface ICategoryService
    {
        Dictionary<string, IEnumerable<AdvertCategoryViewModel>> GetAllCategories();

        bool DoesCategoryExist(string categoryId);
    }
}
