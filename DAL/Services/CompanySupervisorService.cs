using DAL.Entities;
namespace DAL.Services
{
    public class CompanySupervisorService : ICompanySupervisorService
    {
        private readonly AppDbContext _context;
        public CompanySupervisorService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<CompanySupervisor> GetAll()
        {
            return _context.CompanySupervisors;
        }
        public IQueryable<CompanySupervisor> GetById(int CompanySupervisorId)
        {
            return _context.CompanySupervisors.
                Where(x => x.CompanySupervisorId == CompanySupervisorId);
        }
        public IQueryable<CompanySupervisor> GetByUserId(int UserId)
        {
            return _context.CompanySupervisors.
                Where(x => x.UserId == UserId);
        }
        public IQueryable<CompanySupervisor> GetByCompanyId(int CompanyId)
        {
            return _context.CompanySupervisors.
                Where(x => x.CompanyId == CompanyId);
        }
        public async Task Add(CompanySupervisor Supervisor)
        {
            _context.CompanySupervisors.Add(Supervisor);
            await _context.SaveChangesAsync();
        }
        public async Task Update(CompanySupervisor Supervisor)
        {
            _context.CompanySupervisors.Update(Supervisor);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int CompanySupervisorId)
        {
            var Supervisor = await _context.CompanySupervisors.
                FindAsync(CompanySupervisorId);
            if(Supervisor != null)
            {
                _context.CompanySupervisors.Remove(Supervisor);
                await _context.SaveChangesAsync();
            }
        }
        


    }
}
