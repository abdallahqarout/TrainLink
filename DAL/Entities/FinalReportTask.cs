using System;


namespace DAL.Entities
{
    public class FinalReportTask
    {
        public int FinalReportTaskId { get; set; } // Primary key
        public int FinalReportId { get; set; } // Foreign key to FinalReport
        public string Title { get; set; } // Task title
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; } // Task description
        public string Contributions { get; set; }
        public string HardwareUsed { get; set; }
        public string SoftwareEnvironment { get; set; }
        public FinalReport FinalReport { get; set; } // Navigation property to the FinalReport entity
    }
}
