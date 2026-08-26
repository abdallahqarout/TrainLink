using DAL.Entities;
namespace DAL.Services
{
    public interface ICompanySupervisorService
    {
        IQueryable<CompanySupervisor> GetAll();
        IQueryable<CompanySupervisor> GetById(int companySupervisorId);
        IQueryable<CompanySupervisor> GetByUserId(int userId);
        IQueryable<CompanySupervisor> GetByCompanyId(int companyId);
        Task Add(CompanySupervisor Supervisor);
        Task Update(CompanySupervisor Supervisor);
        Task Delete(int companySupervisorId);
    }
}
