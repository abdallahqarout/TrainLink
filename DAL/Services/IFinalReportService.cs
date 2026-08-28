using DAL.Entities;
namespace DAL.Services
{
    public interface IFinalReportService
    {
        IQueryable<FinalReport> GetAll();
        IQueryable<FinalReport> GetById(int finalReportId);
        IQueryable<FinalReport> GetByTrainingId(int trainingId);

        Task Add(FinalReport Report);
        Task Update(FinalReport Report);
        Task Delete (int finalReportId);
    }
}
