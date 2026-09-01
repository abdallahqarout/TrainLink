using DAL.Entities;

namespace DAL.Services
{
    public interface IStudentService
    {
        IQueryable<Student> GetAll();

        IQueryable<Student> GetById(int studentId);

        IQueryable<Student> GetByUserId(int userId);

        IQueryable<Student> GetByUniversityId(int universityId);

        IQueryable<Student> GetByStudentNumber(string studentNumber);

        Task Add(Student student);

        Task Update(Student student);

        Task Delete(int studentId);
    }
}