using DAL.Entities;
namespace DAL.Services
{
    public interface ICompanyService
    {
        IQueryable<Company> GetAll();
        IQueryable<Company> GetById(int companyId);
        IQueryable<Company> GetByName(string name);
        Task Add(Company company);
        Task Update(Company company);
        Task Delete(int companyId);

    }
}
