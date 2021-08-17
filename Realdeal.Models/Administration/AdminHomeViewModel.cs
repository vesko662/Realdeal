namespace Realdeal.Models.Administration
{
    public class AdminHomeViewModel
    {
        public int TotalUsers { get; set; }
        public int RegistratedTodayUsers { get; set; }
        public int TotalAdverts { get; set; }
        public int CreatetTodayAdverts { get; set; }
        public int TotalDeletedAdverts { get; set; }
        public int CreatedReportsToday { get; set; }
        public int CreatedFeedbackToday { get; set; }
    }
}
