using System;
namespace DAL.Entities
{
    public class WeeklyReport
    {
        public int WeeklyReportId { get; set; } // Primary key
        public int TrainingId { get; set; } // Foreign key 
        public int WeekNumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Tasks { get; set; }
        public string Skills { get; set; }
        public string Remark { get; set; }
        public DateTime submissionDAte { get; set; }
        public Training Training { get; set; } // Navigation property to the Training entity
        public ICollection<ReportReview> ReportReviews { get; set; } // Navigation property to the ReportReview entity

    }
}
