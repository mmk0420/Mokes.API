using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task RemoveAsync(User user);
        Task UpdateAsync(User user);
    }
}
