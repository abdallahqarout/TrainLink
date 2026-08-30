using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.Services
{
    public class TrainingService : ITrainingService  
    {
        private readonly AppDbContext _context;

        public TrainingService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Training> GetAll()
        {
            return _context.Trainings
                .Include(t => t.Student)
                    .ThenInclude(s => s.User)
                .Include(t => t.Doctor)
                    .ThenInclude(d => d.User)
                .Include(t => t.Company)
                .Include(t => t.CompanySupervisor)
                    .ThenInclude(cs => cs.User);
        }

        public IQueryable<Training> GetById(int trainId)
        {
            return _context.Trainings
                .Include(t => t.Student)
                .Include(t => t.Doctor)
                .Include(t => t.Company)
                .Include(t => t.CompanySupervisor)
                .Where(t => t.TrainingId == trainId);
        }

        public IQueryable<Training> GetByStudentId(int studentId)
        {
            return _context.Trainings
                .Where(t => t.StudentId == studentId)
                .Include(t => t.Student)
                    .ThenInclude(s => s.User)
                .Include(t => t.Doctor)
                    .ThenInclude(d => d.User)
                .Include(t => t.Company)
                .Include(t => t.CompanySupervisor)
                    .ThenInclude(cs => cs.User);
        }

        public IQueryable<Training> GetByDoctorId(int doctorId)
        {
            return _context.Trainings
                .Where(t => t.DoctorId == doctorId)
                .Include(t => t.Student)
                    .ThenInclude(s => s.User)
                .Include(t => t.Doctor)
                    .ThenInclude(d => d.User)
                .Include(t => t.Company)
                .Include(t => t.CompanySupervisor)
                    .ThenInclude(cs => cs.User);
        }

        public IQueryable<Training> GetByCompanyId(int companyId)
        {
            return _context.Trainings
                .Where(t => t.CompanyId == companyId);
        }

        public IQueryable<Training> GetByCompanySupervisorId(int companySupervisorId)
        {
            return _context.Trainings
                .Where(t => t.CompanySupervisorId == companySupervisorId)
                .Include(t => t.Student)
                    .ThenInclude(s => s.User)
                .Include(t => t.Doctor)
                    .ThenInclude(d => d.User)
                .Include(t => t.Company)
                .Include(t => t.CompanySupervisor)
                    .ThenInclude(cs => cs.User);
        }

        public async Task Add(Training training)
        {
            await _context.Trainings.AddAsync(training);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Training training)
        {
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int trainId)
        {
            var training = await _context.Trainings.FindAsync(trainId);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
        }
    }
}
