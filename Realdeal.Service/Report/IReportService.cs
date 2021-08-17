using Realdeal.Models.Report;
using System.Collections.Generic;

namespace Realdeal.Service.Report
{
    public interface IReportService
    {
        public bool ReportAdvert(AdvertReportFormModel advertReport);
        public void Feedback(FeedbackFormModel feedback);

        IEnumerable<FeedbackViewModel> GetAllFeedbacks();
        void FeedbackIsDone(string feedbackId);

        IEnumerable<ReportViewModel> GetAllReports();
        void ReportIsDone(string reportId);

        public int GetNewestReportsCount();
        public int GetNewestFeedbacksCount();
    }
}
