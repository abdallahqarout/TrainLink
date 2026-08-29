using System;

namespace DAL.Entities
{
    public class Training
    {
        public int TrainingId { get; set; }
        public int StudentId { get; set; }
        public int DoctorId { get; set; }
        public int CompanyId { get; set; }
        public int CompanySupervisorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Student Student { get; set; }
        public Doctor Doctor { get; set; }
        public CompanySupervisor CompanySupervisor { get; set; }
        public ICollection<WeeklyReport> WeeklyReports { get; set; }
        public ICollection<FinalReport> FinalReports { get; set; }
    }
}