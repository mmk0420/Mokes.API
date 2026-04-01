using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _database;
        public UserRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task AddAsync(User user)
        {
            _database.Users.Add(user);
            await _database.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _database.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _database.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RemoveAsync(User user)
        {
            _database.Users.Remove(user);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _database.Users.Update(user);
            await _database.SaveChangesAsync();
        }
    }
}
