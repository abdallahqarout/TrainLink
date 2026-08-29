using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class FinalReport
    {
        public int FinalReportId { get; set; }
        public int TrainingId { get; set; }
        public string Acknowledgement { get; set; }
        public string CompanyBackground { get; set; }
        public string companyProducts { get; set; }
        public string CompanyStructure { get; set; }
        public string FormalReceived { get; set; }
        public string RelateExpToStudy { get; set; }
        public string Conclusion { get; set; }
        public DateTime SubmissionDate { get; set; }
        // Relationship
        public Training Training { get; set; }
        // Final Report Sections
        public ICollection<FinalReportTask> Tasks { get; set; }
        public ICollection<FinalReportReference> References { get; set; }
        public ICollection<FinalReportAppendix> Appendices { get; set; }
        public ICollection<ReportReview> ReportReviews { get; set; }
    }
}