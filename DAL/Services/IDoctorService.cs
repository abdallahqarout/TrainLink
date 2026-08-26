using DAL.Entities;
namespace DAL.Services
{
    public interface IDoctorService
    {
        IQueryable<Doctor> GetAll();
        IQueryable<Doctor> GetById(int doctorId);
        IQueryable<Doctor> GetByUserId(int userId);
        IQueryable<Doctor> GetByUniversityId(int universityId);

        Task Add(Doctor doctor);
        Task Update(Doctor doctor);
        Task Delete(int doctorId);


    }
}
