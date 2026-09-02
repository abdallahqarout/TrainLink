using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class WeeklyReportService : IWeeklyReportService
    {
        private readonly AppDbContext _context;

        public WeeklyReportService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<WeeklyReport> GetAll()
        {
            return _context.WeeklyReports
                .Include(x => x.Training)
                    .ThenInclude(x => x.Student);
        }

        public IQueryable<WeeklyReport> GetById(int weeklyReportId)
        {
            return _context.WeeklyReports
                .Include(x => x.Training)
                    .ThenInclude(x => x.Student)
                .Where(x => x.WeeklyReportId == weeklyReportId);
        }

        public IQueryable<WeeklyReport> GetByTrainingId(int trainingId)
        {
            return _context.WeeklyReports
                .Include(x => x.Training)
                    .ThenInclude(x => x.Student)
                .Where(x => x.TrainingId == trainingId);
        }

        public IQueryable<WeeklyReport> GetWeekNumber(
            int trainingId,
            int weekNumber)
        {
            return _context.WeeklyReports
                .Where(x =>
                    x.TrainingId == trainingId &&
                    x.WeekNumber == weekNumber);
        }

        public async Task Add(WeeklyReport weeklyReport)
        {
            _context.WeeklyReports.Add(weeklyReport);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WeeklyReport weeklyReport)
        {
            _context.WeeklyReports.Update(weeklyReport);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int reportId)
        {
            var report = await _context.WeeklyReports
                .FindAsync(reportId);

            if (report != null)
            {
                _context.WeeklyReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}