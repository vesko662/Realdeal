using Microsoft.AspNetCore.Http;
using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Service.Advert;
using Realdeal.Service.CloudinaryCloud;
using Realdeal.Service.Message;
using Realdeal.Service.Observe;
using Realdeal.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class AdvertServiceTest
    {
        private RealdealDbContext context;
        public AdvertServiceTest()
        {
          context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void CreateAdvert_ShouldCreateAdvert()
        {
            var expexted = 1;
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            moqUserServie.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqIFormFile = new Mock<IFormFile>();
            moqCloudinaryService.Setup(x => x.UploadPhoto(moqIFormFile.Object, "folder"))
                .Returns("http://image.bg");

            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();

            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);


            var createAdvert = new AdvertFormModel()
            {
                Name = "name",
                CategoryId = guid,
                Price = 5,
                Images=new List<IFormFile>(),
            };

            advertService.CreateAdvert(createAdvert);

            Assert.Equal(expexted, context.Adverts.Count());
        }

        [Fact]
        public void DeleteAdvert_Valid_ShouldDeleteAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));
            moqObserveService.Setup(x => x.RemoveAllObservingUsers("advertId"));

            var moqMessageService = new Mock<IMessageService>();
            moqMessageService.Setup(x => x.DeleteAllMessagesToAdvert("advertId"));

            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description="des",
                Price = 5,
            };

            context.Adverts.Add(advert);

            var result = advertService.DeleteAdvert(guid);

            Assert.True(result);
        }
        [Fact]
        public void DeleteAdvert_Invalid_ShouldNotDeleteAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();

            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));
            moqObserveService.Setup(x => x.RemoveAllObservingUsers("advertId"));

            var moqMessageService = new Mock<IMessageService>();
            moqMessageService.Setup(x => x.DeleteAllMessagesToAdvert("advertId"));

            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
            };

            context.Adverts.Add(advert);

            var result = advertService.DeleteAdvert("Id");

            Assert.False(result);
        }
        [Fact]
        public void FindAdvertToEdit_ShouldReturnAdvertEditFormModel()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();
          

            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId="Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.FindAdvertToEdit(guid);

            Assert.Equal(result.Id,guid);
            Assert.IsType<AdvertEditFormModel>(result);
        }
        [Fact]
        public void EditAdvert_Valid_ShouldEditAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var editAdvert = new AdvertEditFormModel()
            {
                Id = guid,
                Name = "Name",
                Description = "Sec",
                Price = 5,
                CategoryId = "Id",
            };

            var result = advertService.EditAdvert(editAdvert);

            Assert.True(result);
        }

        [Fact]
        public void EditAdvert_Invalid_ShouldNotEditAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var editAdvert = new AdvertEditFormModel()
            {
                Id = "Id",
            };

            var result = advertService.EditAdvert(editAdvert);

            Assert.False(result);
        }

        [Fact]
        public void GetUserAdvertById_ShouldReturnUserAdvertsModel()
        {
            var advertId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            moqUserServie.Setup(x => x.GetUserInfo("userId"))
                .Returns(() => null);
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var user = new ApplicationUser()
            {
                Id = userId,
            };

            var advert = new Advert()
            {
                Id = advertId,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
                UserId=userId,
            };
            advert.AdvertImages.Add(new AdvertImage() { ImageUrl = "img.url" });

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetUserAdvertById(userId);

            Assert.IsType<UserAdvertsModel>(result);
            Assert.True(result.Adverts.All(x=>x.Id==advertId));
        }
        [Fact]
        public void GetAdvertDetailsById_ShouldReturnAdvertDetailViewModel()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            moqUserServie.Setup(x => x.GetUserInfo("userId"))
                .Returns(() => null);
            moqUserServie.Setup(x => x.GetCurrentUserId())
                .Returns("userId");
            moqUserServie.Setup(x => x.GetUserIdByAdvertId("advertId"))
                .Returns("Id");

            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetAdvertDetailsById(guid);

            Assert.IsType<AdvertDetailViewModel>(result);
            Assert.Equal(advert.Name, result.Name);
        }
        [Fact]
        public void GetAllAdvertsCount_ShouldReturnAdvertsCount()
        {
            var expected = 1;
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();

            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetAllAdvertsCount();

            Assert.Equal(expected,result);
        }
        [Fact]
        public void GetNewestAdvertsCount_ShouldReturnNewestAdversCount()
        {
            var expected = 1;
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetNewestAdvertsCount();

            Assert.Equal(expected, result);
        }
        [Fact]
        public void GetDeletedAdvertsCount_ShouldReturnDeletedAdversCount()
        {
            var expected = 1;
            var guid = Guid.NewGuid().ToString();

            var moqUserServie = new Mock<IUserService>();
            var moqCloudinaryService = new Mock<ICloudinaryService>();
            var moqObserveService = new Mock<IObserveService>();
            var moqMessageService = new Mock<IMessageService>();


            var advertService = new AdvertService(context,
                moqCloudinaryService.Object,
                moqUserServie.Object,
                moqObserveService.Object,
                moqMessageService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                SubCategoryId = "Id",
                IsDeleted=true,
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetDeletedAdvertsCount();

            Assert.Equal(expected, result);
        }
    }
}
