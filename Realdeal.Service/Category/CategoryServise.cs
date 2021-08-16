using Microsoft.EntityFrameworkCore;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Category;
using Realdeal.Service.CloudinaryCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using static Realdeal.Common.GlobalConstants;

namespace Realdeal.Service.Category
{
    public class CategoryServise : ICategoryService
    {
        private readonly RealdealDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public CategoryServise(RealdealDbContext context, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public void CreateMainCategory(CreateMainCategoryFormModel createMainCategory)
        {
            var category = new MainCategory()
            {
                Name = createMainCategory.Name,
                CategoryImage = cloudinaryService.UploadPhoto(createMainCategory.CategoryImg, cloudFolderFormainCategoryImage),
            };

            context.MainCategories.Add(category);

            context.SaveChanges();
        }

        public void CreateSubCategory(CreateSubCategoryFormModel createSubCategory)
        {

            var category = new SubCategory()
            {
                Name = createSubCategory.Name,
                MainCategoryId= createSubCategory.MainCategoryId
            };

            context.SubCategories.Add(category);

            context.SaveChanges();
        }

        public void DeleteMainCategory(string categoryId)
        {
            var category = context.MainCategories
                .Include(x => x.SubCategories)
                .Where(x => x.Id == categoryId)
                .FirstOrDefault();

            if (category == null)
                return;

            foreach (var subCat in category.SubCategories)
            {
                subCat.MainCategoryId = null;
            }

            category.IsDeleted = true;
            category.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();
        }

        public void DeleteSubCategory(string categoryId)
        {
            var category = context.SubCategories
                .Where(x => x.Id == categoryId)
                .FirstOrDefault();

            if (category == null)
                return;


            category.IsDeleted = true;
            category.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();
        }

        public bool DoesSubCategoryExist(string categoryId)
        => this.context.SubCategories.Any(c => c.Id == categoryId);

        public void EditMainCategory(CategoryEditFormModel categoryModel)
        {
            if (IsMainCategoryNameTaken(categoryModel.Name))
                return;

            var category = context.MainCategories
                .Where(x => x.Id == categoryModel.Id)
                .FirstOrDefault();

            if (category == null)
                return;

            category.Name = categoryModel.Name;

            context.SaveChanges();
        }

        public void EditSubCategory(CategoryEditFormModel categoryModel)
        {
            if (IsSubCategoryNameTaken(categoryModel.Name))
                return;

            var category = context.SubCategories
                .Where(x => x.Id == categoryModel.Id)
                .FirstOrDefault();

            if (category == null)
                return;

            category.Name = categoryModel.Name;

            context.SaveChanges();
        }

        public IEnumerable<CategoriesShowingViewModel> GetAllCategories()
        => context.MainCategories
                   .Where(x => x.IsDeleted == false)
                   .Select(s => new CategoriesShowingViewModel
                   {
                       Id = s.Id,
                       Name = s.Name,
                       ImgUrl = s.CategoryImage,
                       SubCategories = s.SubCategories
                       .Where(x => x.IsDeleted == false)
                       .Select(c => new CategoryModel
                       {
                           Id = c.Id,
                           Name = c.Name
                       })
                   })
                   .ToList();

        public IEnumerable<CategoryModel> GetMainCategories()
        => context.MainCategories
            .Where(x => x.IsDeleted == false)
            .Select(s => new CategoryModel
            {
                Id = s.Id,
                Name = s.Name,
            })
            .ToList();

        public bool IsMainCategoryNameTaken(string name)
        => context.MainCategories.Any(x => x.Name == name);

        public bool IsSubCategoryNameTaken(string name)
        => context.SubCategories.Any(x => x.Name == name);
    }
}
