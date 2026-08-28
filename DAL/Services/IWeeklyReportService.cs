using DAL.Entities;
namespace DAL.Services
{
    public interface IWeeklyReportService
    {
        IQueryable<WeeklyReport> GetAll();
        IQueryable<WeeklyReport> GetById(int reportId);
        IQueryable<WeeklyReport> GetByTrainingId(int trainingId);
        IQueryable<WeeklyReport> GetWeekNumber (int trainingId, int weekNumber);
        Task Add(WeeklyReport weeklyReport);
        Task Update(WeeklyReport weeklyReport);
        Task Delete(int reportId);
    }
}
