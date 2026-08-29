namespace DAL.Entities
{
    public class FinalReportReference
    {
        public int FinalReportReferenceId { get; set; }
        public int FinalReportId { get; set; }
        public string ReferenceText { get; set; }
        // Relationship
        public FinalReport FinalReport { get; set; }
    }
}