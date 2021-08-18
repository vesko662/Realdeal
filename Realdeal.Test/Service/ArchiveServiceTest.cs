using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Archive;
using Realdeal.Service.Archive;
using Realdeal.Service.Observe;
using Realdeal.Service.User;
using System;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class ArchiveServiceTest
    {
        private RealdealDbContext context;

        public ArchiveServiceTest()
        {
            context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void AddAdvertToArchive_Valid_ShouldAddAdvertToArchive()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.AddAdvertToArchive(guid);

            Assert.True(result);
        }
        [Fact]
        public void AddAdvertToArchive_Invalid_ShouldNotAddAdvertToArchive()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.AddAdvertToArchive("Id");

            Assert.False(result);
        }
        [Fact]
        public void UploadAdvert_Valid_ShouldRemoveAdvertFromArchiveAndUploadAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                IsАrchived=true,
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.UploadAdvert(guid);

            Assert.True(result);
        }
        [Fact]
        public void UploadAdvert_Invalid_ShouldNotRemoveAdvertFromArchiveAndNotUploadAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            var moqObserveService = new Mock<IObserveService>();
            moqObserveService.Setup(x => x.SendEmailOUpdate("advertId", "title", "content"));

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                IsАrchived = true,
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.UploadAdvert("Id");

            Assert.False(result);
        }
        [Fact]
        public void GetArchivedAdvert_Valid_ShouldReturnArchivedAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var moqObserveService = new Mock<IObserveService>();

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var user = new ApplicationUser()
            {
                Id = "userId",
            };
            context.Users.Add(user);
            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                IsАrchived = true,
                UserId = "userId",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetArchivedAdvert(guid);

            Assert.IsType<ArchiveAdvertDetailModel>(result);
            Assert.Equal(guid, result.Id);
        }
        [Fact]
        public void GetArchivedAdvert_Invalid_ShouldNotReturnArchivedAdvert()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var moqObserveService = new Mock<IObserveService>();

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var advert = new Advert()
            {
                Id = guid,
                Name = "name",
                Description = "des",
                Price = 5,
                IsАrchived = true,
                UserId = "userId",
            };

            context.Adverts.Add(advert);

            context.SaveChanges();

            var result = advertService.GetArchivedAdvert("");

            Assert.Null(result);
        }
        [Fact]
        public void IsArchiveFull_ShouldReturnTrueIfArchiveFull()
        {
            var guid = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var moqObserveService = new Mock<IObserveService>();

            var advertService = new ArchiveService(context,
                moqUserService.Object,
                moqObserveService.Object);

            var adverts = Enumerable
                .Range(0, 9)
                .Select(x => new Advert() { UserId = "userId",IsАrchived=true })
                .ToList();

            context.Adverts.AddRange(adverts);

            context.SaveChanges();

            var result = advertService.IsArchiveFull();

            Assert.True(result);
        }
    }
}
