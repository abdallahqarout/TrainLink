using System;

namespace DAL.Entities
{
    public class WeeklyReport
    {
        public int WeeklyReportId { get; set; }
        public int TrainingId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tasks { get; set; }
        public string Skills { get; set; }
        public string Remark { get; set; }
        public string Challenges { get; set; }
        public decimal HoursWorked { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Training Training { get; set; }
        public ICollection<ReportReview> ReportReviews { get; set; }
    }
}