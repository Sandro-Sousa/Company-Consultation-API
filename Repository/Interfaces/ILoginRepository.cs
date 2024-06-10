
using Entities.Entites;

namespace Repository.Interfaces
{
    public interface ILoginRepository
    {
        Task<User?> GetUserByEmail(string email);

        Task<bool> EmailExists(string email);
        Task SaveUser(User user);

        Task UpdateUser(User user);
    }
}
