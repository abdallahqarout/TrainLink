using System;


namespace DAL.Entities
{
    public class FinalReportAppendix
    {
        public int FinalReportAppendixId { get; set; } // Primary key
        public int FinalReportId { get; set; } // Foreign key to FinalReport
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; } // Path to the appendix file
        public FinalReport FinalReport { get; set; } // Navigation property to the FinalReport entity
    }
}
