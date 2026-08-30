namespace DAL.Entities
{
    public class FinalReportAppendix
    {
        public int FinalReportAppendixId { get; set; }
        public int FinalReportId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public FinalReport? FinalReport { get; set; }
    }
}