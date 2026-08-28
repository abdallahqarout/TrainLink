using DAL.Entities;
namespace DAL.Services
{
    public interface IFinalReportTaskService
    {
        IQueryable<FinalReportTask> GetAll();
        IQueryable<FinalReportTask> GetById(int finalReportTaskId);
        IQueryable<FinalReportTask> GetByFinalReportId(int finalReportId);
        
        Task Add(FinalReportTask Task);
        Task Update(FinalReportTask Task);
        Task Delete(int finalReportTaskId);

    }
}
