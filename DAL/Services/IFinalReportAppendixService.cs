using DAL.Entities;
namespace DAL.Services
{
    public interface IFinalReportAppendixService
    {
        IQueryable<FinalReportAppendix> GetAll();
        IQueryable<FinalReportAppendix> GetById(int finalReportAppendixId);
        IQueryable<FinalReportAppendix> GetByFinalReportId(int finalReportId);

        Task Add(FinalReportAppendix finalReportAppendix);
        Task Update(FinalReportAppendix finalReportAppendix);
        Task Delete(int finalReportAppendixId);

    }
}
