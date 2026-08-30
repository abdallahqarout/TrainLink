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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public Student? Student { get; set; }
        public Doctor? Doctor { get; set; }
        public Company? Company { get; set; }
        public CompanySupervisor? CompanySupervisor { get; set; }
        public ICollection<WeeklyReport> WeeklyReports { get; set; } = new List<WeeklyReport>();

        public ICollection<FinalReport> FinalReports { get; set; } = new List<FinalReport>();
    }
}