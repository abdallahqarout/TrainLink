using System;

namespace DAL.Entities
{
    public class FinalReportReference
    {
        public int FinalReportReferenceId { get; set; } // Primary key
        public int FinalReportId { get; set; } // Foreign key to FinalReport
        public string ReferenceText { get; set; } // Reference text
        public FinalReport FinalReport { get; set; }// Navigation property to the FinalReport entity
    }
}
