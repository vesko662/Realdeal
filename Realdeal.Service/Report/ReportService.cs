using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Report;
using Realdeal.Service.User;

namespace Realdeal.Service.Report
{
    public class ReportService : IReportService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;

        public ReportService(RealdealDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public void Feedback(FeedbackFormModel feedbackModel)
        {
            var feedback = new Feedback()
            {
                Description = feedbackModel.Description,
            };

            context.Feedbacks.Add(feedback);

            context.SaveChanges();
        }

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
    }
}
