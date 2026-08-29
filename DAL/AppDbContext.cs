using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySupervisor> CompanySupervisors { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<WeeklyReport> WeeklyReports { get; set; }  
        public DbSet<FinalReport> FinalReports { get; set; }
        public DbSet<FinalReportReference> FinalReportReferences { get; set; }
        public DbSet<FinalReportAppendix> FinalReportAppendices { get; set; }
        public DbSet<FinalReportTask> FinalReportTasks { get; set; }
        public DbSet<ReportReview> ReportReviews { get; set; }
        



    }
}