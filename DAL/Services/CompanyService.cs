using DAL.Entities;

namespace DAL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Company> GetAll()
        {
            return _context.Companies;
        }
        public IQueryable<Company> GetById(int companyId)
        { 
            return _context.Companies.Where(x => x.CompanyId == companyId);         
        }
        public IQueryable<Company> GetByName(string name)
        {
            return _context.Companies.Where(x => x.Name == name);
        }
        public async Task Add(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Company company)
        { 
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();

        }
        public async Task Delete(int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }





    }
}
