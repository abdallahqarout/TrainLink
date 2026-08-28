using DAL.Entities;
namespace DAL.Services
{
    public interface ITrainingService
    {
        IQueryable<Training> GetAll();
        IQueryable<Training> GetById(int trainId);
        IQueryable<Training> GetByStudentId(int studentId);
        IQueryable<Training> GetByDoctorId(int doctorId);
        IQueryable<Training> GetByCompanyId(int companyId);
        IQueryable<Training> GetByCompanySupervisorId(int companySupervisorId);

        Task Add(Training training);
        Task Update(Training training);
        Task Delete(int trainId);
        
    }
}
