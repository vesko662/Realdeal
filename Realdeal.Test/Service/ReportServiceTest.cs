using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Report;
using Realdeal.Service.Report;
using Realdeal.Service.User;
using System;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class ReportServiceTest
    {
        private RealdealDbContext context;

        public ReportServiceTest()
        {
            context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void CreateFeedback_ShouldCreteFeedback()
        {
            var expexted = 1;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var reportService = new ReportService(context, moqUserService.Object);

            var feedback = new FeedbackFormModel()
            {
                Description = "des",
            };

            reportService.CreateFeedback(feedback);
            var resul = context.Feedbacks.Count();
            Assert.Equal(expexted, resul);
        }

        [Fact]
        public void FeedbackIsDone_ShouldBeDone()
        {
            var guid = Guid.NewGuid().ToString();
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var feedback = new Feedback()
            {
                Id=guid,
                Description = "des",
            };

            context.Feedbacks.Add(feedback);
            context.SaveChanges();

            reportService.FeedbackIsDone(guid);
            var resul = context.Feedbacks.Find(guid).IsDone;
            Assert.True(resul);
        }

        [Fact]
        public void FeedbackIsDone_InvalidFeedbackId_ShouldNotBeDone()
        {
            var guid = Guid.NewGuid().ToString();
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var feedback = new Feedback()
            {
                Id = guid,
                Description = "des",
            };

            context.Feedbacks.Add(feedback);
            context.SaveChanges();

            reportService.FeedbackIsDone("wrongId");
            var resul = context.Feedbacks.Find(guid).IsDone;
            Assert.False(resul);
        }

        [Fact]
        public void GetAllFeedbacks_ShouldGetAllNotDoneFeedbacks()
        {
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var user = new ApplicationUser()
            {
                Id = "userId",
                UserName="username"
            };
            context.Users.Add(user);

            var feedback = new Feedback()
            {
                Id = "1",
                Description = "des",
                MakerId= "userId",
            };
            var feedback1 = new Feedback()
            {
                Id = "2",
                Description = "des",
                IsDone = true,
                MakerId = "userId",
            };
            var feedback2 = new Feedback()
            {
                Id = "3",
                Description = "des",
                MakerId = "userId",
            };

            context.Feedbacks.AddRange(feedback,feedback1,feedback2);

            context.SaveChanges();

            var expected = context.Feedbacks
                .Where(x => x.IsDone == false)
                .Count();

            var result = reportService
                .GetAllFeedbacks()
                .Count();

            Assert.Equal(expected,result);
        }
        [Fact]
        public void GetAllReports_ShouldGetAllNotDoneReports()
        {
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var advert = new Advert()
            {
                Id = "advertID",
                Name = "name",
            };
            context.Adverts.Add(advert);

            var report = new AdvertReport()
            {
                Id = "1",
                Description = "des",
                AdvertId= "advertID"
            };
            var report1 = new AdvertReport()
            {
                Id = "2",
                Description = "des",
                IsDone = true,
                AdvertId = "advertID"
            };
            var report2 = new AdvertReport()
            {
                Id = "3",
                Description = "des",
                AdvertId = "advertID",
                IsDone=true,
            };

            context.ReporedAdverts.AddRange(report, report1, report2);

            context.SaveChanges();

            var expected = context.ReporedAdverts
                .Where(x => x.IsDone == false)
                .Count();

            var result = reportService
                .GetAllReports()
                .Count();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReportAdvert_ShouldCreateAdvertReport()
        {
            var expexted = 1;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var reportService = new ReportService(context, moqUserService.Object);

            var report = new AdvertReportFormModel()
            {
                Description = "des",
                AdvertId="advert"
            };

            var advert = new Advert()
            {
                Id = "advert",
            };
            context.Adverts.Add(advert);
            context.SaveChanges();

            reportService.ReportAdvert(report);
            var resul = context.Adverts
                .Find("advert")
                .AdvertReports
                .Count();
            Assert.Equal(expexted, resul);
        }
        [Fact]
        public void ReportAdvert_InvalidAdvertId_ShouldNotCreateAdvertReport()
        {
            var expexted = 0;
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetCurrentUserId())
                .Returns("userId");

            var reportService = new ReportService(context, moqUserService.Object);

            var report = new AdvertReportFormModel()
            {
                Description = "des",
                AdvertId = "wrong"
            };

            var advert = new Advert()
            {
                Id = "advert",
            };
            context.Adverts.Add(advert);
            context.SaveChanges();

            reportService.ReportAdvert(report);
            var resul = context.Adverts
                .Find("advert")
                .AdvertReports
                .Count();
            Assert.Equal(expexted, resul);
        }

        [Fact]
        public void ReportIsDone_ShouldBeDone()
        {
            var guid = Guid.NewGuid().ToString();
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var report = new AdvertReport()
            {
                Id = guid,
                Description = "des",
            };

            context.ReporedAdverts.Add(report);
            context.SaveChanges();

            reportService.ReportIsDone(guid);
            var resul = context.ReporedAdverts.Find(guid).IsDone;
            Assert.True(resul);
        }

        [Fact]
        public void ReportIsDone_InvalidReportId_ShouldNotBeDone()
        {
            var guid = Guid.NewGuid().ToString();
            var moqUserService = new Mock<IUserService>();

            var reportService = new ReportService(context, moqUserService.Object);

            var report = new AdvertReport()
            {
                Id = guid,
                Description = "des",
            };

            context.ReporedAdverts.Add(report);
            context.SaveChanges();

            reportService.ReportIsDone("wrongId");
            var resul = context.ReporedAdverts.Find(guid).IsDone;
            Assert.False(resul);
        }

    }
}
