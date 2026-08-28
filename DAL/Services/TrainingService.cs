using DAL.Entities;
namespace DAL.Services
{
    public class TrainingService : ITrainingService  
    {
        private readonly AppDbContext _context;

        public TrainingService(AppDbContext _context)
        {
            _context = _context;
        }

        public IQueryable<Training> GetAll()
        {
            return _context.Trainings;
        }

        public IQueryable<Training> GetById(int trainId)
        {
            return _context.Trainings
                .Where(t => t.Id == trainId);
        }

        public IQueryable<Training> GetByStudentId(int studentId)
        {
            return _context.Trainings
                .Where(t => t.StudentId == studentId);
        }

        public IQueryable<Training> GetByDoctorId(int doctorId)
        {
            return _context.Trainings
                .Where(t => t.DoctorId == doctorId);
        }

        public IQueryable<Training> GetByCompanyId(int companyId)
        {
            return _context.Trainings
                .Where(t => t.CompanyId == companyId);
        }

        public IQueryable<Training> GetByCompanySupervisorId(int companySupervisorId)
        {
            return _context.Trainings
                .Where(t => t.CompanySupervisorId == companySupervisorId);
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
