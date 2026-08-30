using System;

namespace DAL.Entities
{
    public class FinalReportTask
    {
        public int FinalReportTaskId { get; set; }

        public int FinalReportId { get; set; }

        public string? Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string? Description { get; set; }

        public string? Contributions { get; set; }

        public string? HardwareUsed { get; set; }

        public string? SoftwareEnvironment { get; set; }

        public FinalReport? FinalReport { get; set; }
    }
}