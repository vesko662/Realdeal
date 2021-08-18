using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Report;
using Realdeal.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Report
{
    public class ReportService : IReportService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;

        public ReportService(RealdealDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public void CreateFeedback(FeedbackFormModel feedbackModel)
        {
            var feedback = new Feedback()
            {
                Description = feedbackModel.Description,
                MakerId = userService.GetCurrentUserId(),
            };

            context.Feedbacks.Add(feedback);

            context.SaveChanges();
        }

        public void FeedbackIsDone(string feedbackId)
        {
            var feedback = context.Feedbacks
                .Find(feedbackId);

            if (feedback == null)
                return;

            feedback.IsDone = true;

            context.SaveChanges();
        }

        public IEnumerable<FeedbackViewModel> GetAllFeedbacks()
        => context.Feedbacks
            .Where(x => x.IsDone == false)
            .Select(s => new FeedbackViewModel()
            {
                Id = s.Id,
                CreatorUsername = s.Maker.UserName,
                Description = s.Description,
            })
            .ToList();

        public IEnumerable<ReportViewModel> GetAllReports()
        => context.ReporedAdverts
            .Where(x => x.IsDone == false)
            .Select(s => new ReportViewModel()
            {
                ReportId = s.Id,
                AdvertId = s.AdvertId,
                AdvertName = s.Advert.Name,
                Description = s.Description,
            })
            .ToList();

        public int GetNewestFeedbacksCount()
        => context.Feedbacks
            .Where(x => x.IsDone == false)
            .Where(x => x.CreatedOn.Date == DateTime.UtcNow.Date && x.CreatedOn.Year == DateTime.UtcNow.Year)
            .Count();

        public int GetNewestReportsCount()
        => context.ReporedAdverts
            .Where(x => x.IsDone == false)
            .Where(x => x.CreatedOn.Date == DateTime.UtcNow.Date && x.CreatedOn.Year == DateTime.UtcNow.Year)
            .Count();


        public bool ReportAdvert(AdvertReportFormModel advertReport)
        {
            var advert = context.Adverts.Find(advertReport.AdvertId);

            if (advert == null)
            {
                return false;
            }

            var report = new AdvertReport()
            {
                AdvertId = advertReport.AdvertId,
                Description = advertReport.Description,
                UserId = userService.GetCurrentUserId()
            };

            context.ReporedAdverts.Add(report);

            context.SaveChanges();

            return true;
        }

        public void ReportIsDone(string reportId)
        {
            var report = context.ReporedAdverts
                .Find(reportId);

            if (report == null)
                return;

            report.IsDone = true;

            context.SaveChanges();
        }
    }
}
