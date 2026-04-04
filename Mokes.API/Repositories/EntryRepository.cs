using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _database;
        private IEntryRepository _entryRepositoryImplementation;

        public EntryRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<List<Entry>> GetAllRemovedAsync(Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.DeletedAt != null && e.UserId == userId)
                .ToListAsync();
        }

        public async Task<Entry?> GetRemovedByIdAsync(Guid id, Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.DeletedAt != null && e.UserId == userId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Entry entry)
        {
            _database.Entries.Add(entry);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entry entry)
        {
            _database.Entries.Remove(entry);
            await _database.SaveChangesAsync();
        }

        public async Task<List<Entry>> GetAllAsync(Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Entry?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _database.Entries
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.DeletedAt == null)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Entry entry)
        {
            _database.Entries.Update(entry);
            await _database.SaveChangesAsync();
        }
    }
}
