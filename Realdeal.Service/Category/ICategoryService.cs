using Realdeal.Models.Category;
using System.Collections.Generic;

namespace Realdeal.Service.Category
{
    public interface ICategoryService
    {
        bool DoesSubCategoryExist(string categoryId);

        bool IsMainCategoryNameTaken(string name);
        bool IsSubCategoryNameTaken(string name);

        void DeleteMainCategory(string categoryId);
        void DeleteSubCategory(string categoryId);

        void EditMainCategory(CategoryEditFormModel categoryModel);
        void EditSubCategory(CategoryEditFormModel categoryModel);


        IEnumerable<CategoriesShowingViewModel> GetAllCategories();

        IEnumerable<CategoryModel> GetMainCategories();
        void CreateMainCategory(CreateMainCategoryFormModel createMainCategory);
        void CreateSubCategory(CreateSubCategoryFormModel createSubCategory);
    }
}
