
using DAL.Entities;

namespace DAL.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAll();

        IQueryable<User> GetById(int userId);

        IQueryable<User> GetByEmail(string email);

        Task Add(User user);

        Task Update(User user);

        Task Delete(int userId);
    }
}
