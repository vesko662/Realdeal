using Realdeal.Data;
using Realdeal.Models.Category;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Category
{
    public class CategoryServise : ICategoryService
    {
        private readonly RealdealDbContext context;

        public CategoryServise(RealdealDbContext context)
        {
            this.context = context;
        }

        public bool DoesCategoryExist(string categoryId)
        => this.context.SubCategories.Any(c => c.Id == categoryId);

        public IEnumerable<MainCategoriesShowingViewModel> GetAllCategories()
        => context.MainCategories
                   .Select(s => new MainCategoriesShowingViewModel
                   {
                       Name = s.Name,
                       ImgUrl = s.CategoryImage,
                       SubCategories = s.SubCategories.Select(c => new CategoryModel
                       {
                           Id = c.Id,
                           Name = c.Name
                       })
                   })
                   .ToList();
    }
}
