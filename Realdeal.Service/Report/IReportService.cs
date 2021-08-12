using Realdeal.Models.Report;

namespace Realdeal.Service.Report
{
    public interface IReportService
    {
        public bool ReportAdvert(AdvertReportFormModel advertReport);
        public void Feedback(FeedbackFormModel feedback);
    }
}
