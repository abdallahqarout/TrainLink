using DAL.Entities;

namespace DAL.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetAll()
        {
            return _context.Students;
        }

        public IQueryable<Student> GetById(int studentId)
        {
            return _context.Students
                .Where(x => x.StudentId == studentId);
        }

        public IQueryable<Student> GetByUserId(int userId)
        {
            return _context.Students
                .Where(x => x.UserId == userId);
        }

        public IQueryable<Student> GetByUniversityId(int universityId)
        {
            return _context.Students
                .Where(x => x.UniversityId == universityId);
        }
        public IQueryable<Student> GetByStudentNumber(string studentNumber)
        {
            return _context.Students
                .Where(x => x.StudentNumber == studentNumber);
        }

        public async Task Add(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Student student)
        {
            _context.Students.Update(student);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int studentId)
        {
            var student = await _context.Students
                .FindAsync(studentId);

            if (student != null)
            {
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
            }
        }
    }
}