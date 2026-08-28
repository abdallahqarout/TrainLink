using DAL.Entities;
namespace DAL.Services
{
    public interface IFinalReportReferenceService
    {
        IQueryable<FinalReportReference> GetAll();
        IQueryable<FinalReportReference> GetById(int finalReportReferenceId);
        IQueryable<FinalReportReference> GetByFinalReportId(int finalReportId);

        Task Add(FinalReportReference reference);
        Task Update(FinalReportReference reference);
        Task Delete(int finalReportReferenceId);

    }
}
