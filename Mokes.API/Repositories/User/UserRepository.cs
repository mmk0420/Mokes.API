using Microsoft.EntityFrameworkCore;
using Mokes.API.Data;

namespace Mokes.API.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _database;
        public UserRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task AddAsync(Models.User user)
        {
            _database.Users.Add(user);
            await _database.SaveChangesAsync();
        }

        public async Task<Models.User?> GetByIdAsync(Guid id)
        {
            return await _database.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Models.User?> GetByUsernameAsync(string username)
        {
            return await _database.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RemoveAsync(Models.User user)
        {
            _database.Users.Remove(user);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.User user)
        {
            _database.Users.Update(user);
            await _database.SaveChangesAsync();
        }
    }
}
