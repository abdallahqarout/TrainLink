using DAL.Entities;

namespace DAL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Doctor> GetAll()
        {
            return _context.Doctors;
        }

        public IQueryable<Doctor> GetById(int doctorId)
        {
            return _context.Doctors
                .Where(x => x.DoctorId == doctorId);
        }

        public IQueryable<Doctor> GetByUserId(int userId)
        {
            return _context.Doctors
                .Where(x => x.UserId == userId);
        }

        public IQueryable<Doctor> GetByUniversityId(int universityId)
        {
            return _context.Doctors
                .Where(x => x.UniversityId == universityId);
        }

        public async Task Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);

            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);

                await _context.SaveChangesAsync();
            }
        }
    }
}