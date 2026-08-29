using DAL.Entities;
namespace DAL.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly AppDbContext _context;
        public UniversityService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<University> GetAll()
        {
            return _context.Universities;
        }
        public IQueryable<University> GetById(int universityId)
        {
            return _context.Universities
                .Where(x => x.UniversityId == universityId);
        }
        public IQueryable<University> GetByName(string name)
        {
            return _context.Universities
                .Where(x => x.Name == name);
        }
        public async Task Add(University university)
        {
            await _context.Universities.AddAsync(university);
            await _context.SaveChangesAsync();
        }
        public async Task Update(University university)
        {
            _context.Universities.Update(university);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int universityId)
        {
            var university = await _context.Universities.FindAsync(universityId);
            if (university != null)
            {
                _context.Universities.Remove(university);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(University university)
        {
            var existingUniversity = await _context.Universities
                .FindAsync(university.UniversityId);

            if (existingUniversity != null)
            {
                _context.Universities.Remove(existingUniversity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
