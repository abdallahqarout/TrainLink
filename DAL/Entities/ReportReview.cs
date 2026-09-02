using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ReportReview
    {
        [Key]
        public int ReviewId { get; set; }

        public int? WeeklyReportId { get; set; }

        public int? FinalReportId { get; set; }

        public int ReviewerUserId { get; set; }

        public string? Decision { get; set; }

        public string? Comments { get; set; }

        public DateTime ReviewDate { get; set; }

        public WeeklyReport? WeeklyReport { get; set; }

        public FinalReport? FinalReport { get; set; }

        public User? ReviewerUser { get; set; }
    }
}