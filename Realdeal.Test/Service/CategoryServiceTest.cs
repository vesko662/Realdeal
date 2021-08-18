using Microsoft.AspNetCore.Http;
using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Category;
using Realdeal.Service.Category;
using Realdeal.Service.CloudinaryCloud;
using System;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class CategoryServiceTest
    {
        private RealdealDbContext context;

        public CategoryServiceTest()
        {
            context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void AssignSubCategory_Valid_ShouldAssignSubcategoryToMaincategory()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.AssignSubCategory(mainCatId, subCatId);

            var result = context.MainCategories.Find(mainCatId).SubCategories.ToList();

            Assert.True(result.Exists(x => x.Id == subCategory.Id));
        }
        [Fact]
        public void AssignSubCategory_InvalidMainCatId_ShouldNotAssignSubcategoryToMaincategory()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.AssignSubCategory("", subCatId);

            var result = context.MainCategories.Find(mainCatId).SubCategories.ToList();

            Assert.False(result.Exists(x => x.Id == subCategory.Id));
        }
        [Fact]
        public void AssignSubCategory_SubcategoryNameAlreadyExistInMaincategory_ShouldNotAssignSubcategoryToMaincategory()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();
            var subCat2Id = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
                Name = "name",
                MainCategoryId = mainCatId,
            };

            var subCategory2 = new SubCategory()
            {
                Id = subCat2Id,
                Name = "name",
            };
            context.SubCategories.AddRange(subCategory, subCategory2);

            context.SaveChanges();

            categoryService.AssignSubCategory(mainCatId, subCat2Id);

            var result = context.MainCategories.Find(mainCatId).SubCategories.ToList();

            Assert.False(result.Exists(x => x.Id == subCategory2.Id));
        }
        [Fact]
        public void AssignSubCategory_InvalidSubCatId_ShouldNotAssignSubcategoryToMaincategory()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.AssignSubCategory(mainCatId, "");

            var result = context.MainCategories.Find(mainCatId).SubCategories.ToList();

            Assert.False(result.Exists(x => x.Id == subCategory.Id));
        }
        [Fact]
        public void CreateMainCategory_ShouldCreateMainCategory()
        {
            var expected = 1;

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqIFormFile = new Mock<IFormFile>();
            moqCloudinaryService
                .Setup(x => x.UploadPhoto(moqIFormFile.Object, "folder"))
                .Returns("cat.img");

            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new CreateMainCategoryFormModel()
            {
                Name = "Name",
            };

            categoryService.CreateMainCategory(mainCategory);
            var result = context.MainCategories.Count();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreateSubCategory_ShouldCreateSubCategory()
        {
            var expected = 1;

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCat = new CreateSubCategoryFormModel()
            {
                Name = "Name",
            };

            categoryService.CreateSubCategory(subCat);
            var result = context.SubCategories.Count();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteMainCategory_Valid_ShouldDeleteMainCategoryAndUnassignSubcategories()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
                MainCategoryId = mainCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.DeleteMainCategory(mainCatId);

            Assert.True(context.MainCategories.Find(mainCatId).IsDeleted);
            Assert.Null(context.SubCategories.Find(subCatId).MainCategoryId);
        }
        [Fact]
        public void DeleteMainCategory_InvalidMaincategoryId_ShouldNotDeleteMainCategoryAndNotUnassignSubcategories()
        {
            var mainCatId = Guid.NewGuid().ToString();
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCategory = new MainCategory()
            {
                Id = mainCatId,
            };
            context.MainCategories.Add(mainCategory);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
                MainCategoryId = mainCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.DeleteMainCategory("");

            Assert.False(context.MainCategories.Find(mainCatId).IsDeleted);
            Assert.NotNull(context.SubCategories.Find(subCatId).MainCategoryId);
        }
        [Fact]
        public void DeleteSubCategory_Valid_ShouldDeleteSubcategory()
        {
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.DeleteSubCategory(subCatId);

            Assert.True(context.SubCategories.Find(subCatId).IsDeleted);
        }
        [Fact]
        public void DeleteSubCategory_InvalidSubcategoryId_ShouldDeleteSubcategory()
        {
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

            categoryService.DeleteSubCategory(" ");

            Assert.False(context.SubCategories.Find(subCatId).IsDeleted);
        }
        [Fact]
        public void DoesSubCategoryExist_ShouldReturnTrueIfExist()
        {
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCategory = new SubCategory()
            {
                Id = subCatId,
            };
            context.SubCategories.Add(subCategory);

            context.SaveChanges();

           var result= categoryService.DoesSubCategoryExist(subCatId);

            Assert.True(result);
        }
        [Fact]
        public void EditSubCategory_Valid_ShoulEditSubCategory()
        {
            var expected = "New name";
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCat = new SubCategory()
            {
                Id = subCatId,
                Name="Name",
            };
            context.SubCategories.Add(subCat);

            context.SaveChanges();

            var editSubCat = new CategoryEditFormModel()
            {
                Id = subCatId,
                Name = expected,
            };

            categoryService.EditSubCategory(editSubCat);

            var result = context.SubCategories.Find(subCatId).Name;

            Assert.Equal(expected,result);
        }
        [Fact]
        public void EditSubCategory_SubCategoryIdDoesNotExist_ShoulNotEditSubCategory()
        {
            var expected = "New name";
            var subCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var subCat = new SubCategory()
            {
                Id = subCatId,
                Name = "Name",
            };
            context.SubCategories.Add(subCat);

            context.SaveChanges();

            var editSubCat = new CategoryEditFormModel()
            {
                Id = "nono",
                Name = expected,
            };

            categoryService.EditSubCategory(editSubCat);

            var result = context.SubCategories.Find(subCatId).Name;

            Assert.NotEqual(expected, result);
        }
        [Fact]
        public void EditMainCategory_Valid_ShoulEditMainCategory()
        {
            var expected = "New name";
            var mainCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCat = new MainCategory()
            {
                Id = mainCatId,
                Name="Name",
            };
            context.MainCategories.Add(mainCat);

            context.SaveChanges();

            var editMainCat = new CategoryEditFormModel()
            {
                Id = mainCatId,
                Name = expected,
            };

            categoryService.EditMainCategory(editMainCat);

            var result = context.MainCategories.Find(mainCatId).Name;

            Assert.Equal(expected,result);
        }
        [Fact]
        public void EditMainCategory_MainCategoryIdDoesNotExist_ShoulNotEditMainCategory()
        {
            var expected = "New name";
            var mainCatId = Guid.NewGuid().ToString();

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var categoryService = new CategoryServise(context, moqCloudinaryService.Object);

            var mainCat = new MainCategory()
            {
                Id = mainCatId,
                Name = "Name",
            };
            context.MainCategories.Add(mainCat);

            context.SaveChanges();

            var editMainCat = new CategoryEditFormModel()
            {
                Id = "asad",
                Name = expected,
            };

            categoryService.EditMainCategory(editMainCat);

            var result = context.MainCategories.Find(mainCatId).Name;

            Assert.NotEqual(expected, result);
        }

    }
}
