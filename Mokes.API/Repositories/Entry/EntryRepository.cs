using Microsoft.EntityFrameworkCore;
using Mokes.API.Data;

namespace Mokes.API.Repositories.Entry
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _database;
        public EntryRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<List<Models.Entry>> GetAllBelongingToUserRemovedAsync(Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.DeletedAt != null && e.UserId == userId)
                .ToListAsync();
        }

        public async Task<Models.Entry?> GetByIdBelongingToUserRemovedAsync(Guid id, Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.DeletedAt != null && e.UserId == userId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Models.Entry entry)
        {
            _database.Entries.Add(entry);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteAsync(Models.Entry entry)
        {
            _database.Entries.Remove(entry);
            await _database.SaveChangesAsync();
        }

        public async Task<List<Models.Entry>> GetAllBelongingToUserAsync(Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Models.Entry?> GetByIdBelongingToUserAsync(Guid id, Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.DeletedAt == null)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Models.Entry entry)
        {
            _database.Entries.Update(entry);
            await _database.SaveChangesAsync();
        }
    }
}
