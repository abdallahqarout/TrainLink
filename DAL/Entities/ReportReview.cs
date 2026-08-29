namespace DAL.Entities
{
    public class ReportReview
    {
        public int ReportReviewId { get; set; }
        public int? WeeklyReportId { get; set; }
        public int? FinalReportId { get; set; }
        public int ReviewerUserId { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; }
        // Relationships
        public WeeklyReport WeeklyReport { get; set; }
        public FinalReport FinalReport { get; set; }
        public User ReviewerUser { get; set; }
    }
}