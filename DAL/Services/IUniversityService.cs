using DAL.Entities;
namespace DAL.Services
{
    public interface IUniversityService
    {
        IQueryable<University> GetAll();
        IQueryable<University> GetById(int universityId);
        IQueryable<University> GetByName(string name);
        Task Add(University university);
        Task Update(University university);
        Task Delete(int universityId);

    }
}
