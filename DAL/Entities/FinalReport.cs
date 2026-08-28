using System;
using System.Reflection.Metadata;

namespace DAL.Entities
{
    public class FinalReport
    {
        public int FinalReportId { get; set; } // Primary key
        public int TrainingId { get; set; } // Foreign key to Training
        public String Acknowledgement { get; set; } // Acknowledgement for company final report
        public string CompanyBackground { get; set; } // Company background for company final report
        public string companyproducts { get; set; } // Company products for company final report
        public string CompanyStructure { get; set; } // Company structure for company final report
        public string FormalReceived { get; set; } // Formal received section
        public string RelateExpToStudy { get; set; } // Relation to study section
        public string Conclusion { get; set; } // Conclusion section
        public DateTime SubmissionDate { get; set; } // Submission date
        public Training Training { get; set; } // Navigation property to the Training entity
        public ICollection<FinalReport> Tasks { get; set; }
        public ICollection<FinalReport> Reference { get; set; }
        public ICollection<FinalReport> Appendices { get; set; }
        public ICollection<FinalReport> ReportReviews { get; set; }




    }
}
