using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Service.EmailSender;
using Realdeal.Service.Observe;
using Realdeal.Service.User;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class ObserveServiceTest
    {
        private RealdealDbContext context;

        public ObserveServiceTest()
        {
            context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void IsAdvertObserved_ShouldReturnTrueIfIsItObserve()
        {
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("user");
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var observedAdvert = new ОbservedAdvert()
            {
                UserId = "user",
                AdvertId = "advertId"
            };

            context.ObservedAdverts.Add(observedAdvert);

            context.SaveChanges();

            var result = observeService.IsAdvertObserved("advertId");

            Assert.True(result);
        }
        [Fact]
        public void RemoveAllObservingUsers_ShouldRemoveAllUsersObservingAdvert()
        {
            var expected = 0;
            var moqUserService = new Mock<IUserService>();
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var observedAdvert = new ОbservedAdvert()
            {
                UserId = "user",
                AdvertId = "advertId"
            };

            context.ObservedAdverts.Add(observedAdvert);

            context.SaveChanges();

            observeService.RemoveAllObservingUsers("advertId");

            var result = context.ObservedAdverts
                .Where(x => x.AdvertId == "advertId")
                .Count();

            Assert.Equal(expected, result);
        }
        [Fact]
        public void RemoveAllObservingUsers_InvalidAdvertId_ShouldNotRemoveAllUsersObservingAdvert()
        {
            var expected = 1;
            var moqUserService = new Mock<IUserService>();
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var observedAdvert = new ОbservedAdvert()
            {
                UserId = "user",
                AdvertId = "advertId"
            };

            context.ObservedAdverts.Add(observedAdvert);

            context.SaveChanges();

            observeService.RemoveAllObservingUsers("1");

            var result = context.ObservedAdverts
                .Where(x => x.AdvertId == "advertId")
                .Count();

            Assert.Equal(expected, result);
        }
        [Fact]
        public void StartObservingAdvert_ShouldAddAdvertToUsersObservingAdverts()
        {
            var expected = 1;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("user");
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var advert = new Advert()
            {
                Id = "advertId"
            };

            context.Adverts.Add(advert);

            var user = new ApplicationUser()
            {
                Id = "user",
            };
            context.Users.Add(user);

            context.SaveChanges();

           var result= observeService.StartObservingAdvert("advertId",false);

            var result2 = context.Users
                .Find("user")
                .ОbservedAdverts
                .Count();

            Assert.True(result);
            Assert.Equal(expected, result2);
        }
        [Fact]
        public void StartObservingAdvert_InvalidAdvertId_ShouldNotAddAdvertToUsersObservingAdverts()
        {
            var expected = 0;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("user");
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var advert = new Advert()
            {
                Id = "advertId"
            };

            context.Adverts.Add(advert);

            var user = new ApplicationUser()
            {
                Id = "user",
            };
            context.Users.Add(user);

            context.SaveChanges();

           var resul= observeService.StartObservingAdvert("wrongId", false);

            var result2 = context.Users
                .Find("user")
                .ОbservedAdverts
                .Count();

            Assert.False(resul);
            Assert.Equal(expected, result2);
        }
        [Fact]
        public void StopObservingAdvert_ShouldRemoveAdvertFromUsersObservingAdverts()
        {
            var expected = 0;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("user");
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var advert = new Advert()
            {
                Id = "advertId"
            };

            context.Adverts.Add(advert);

            var observeAdvert = new ОbservedAdvert()
            {
                AdvertId = "advertId",
                UserId = "user"
            };

            context.ObservedAdverts.Add(observeAdvert);

            var user = new ApplicationUser()
            {
                Id = "user",
            };
            context.Users.Add(user);

            context.SaveChanges();

            var resul = observeService.StopObservingAdvert("advertId");

            var result2 = context.Users
                .Find("user")
                .ОbservedAdverts
                .Count();

            Assert.True(resul);
            Assert.Equal(expected, result2);
        }
        [Fact]
        public void StopObservingAdvert_InvalidAdvertId__ShouldNotRemoveAdvertFromUsersObservingAdverts()
        {
            var expected = 1;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("user");
            var moqEmailService = new Mock<IEmailSenderService>();

            var observeService = new ObserveService(context, moqUserService.Object, moqEmailService.Object);

            var advert = new Advert()
            {
                Id = "advertId"
            };

            context.Adverts.Add(advert);

            var observeAdvert = new ОbservedAdvert()
            {
                AdvertId = "advertId",
                UserId = "user"
            };

            context.ObservedAdverts.Add(observeAdvert);

            var user = new ApplicationUser()
            {
                Id = "user",
            };
            context.Users.Add(user);

            context.SaveChanges();

            var resul = observeService.StopObservingAdvert("wrongId");

            var result2 = context.Users
                .Find("user")
                .ОbservedAdverts
                .Count();

            Assert.False(resul);
            Assert.Equal(expected, result2);
        }
    }
}
