using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _database;
        public EntryRepository(AppDbContext database)
        {
            _database = database;
        }
        public async Task AddAsync(Entry entry)
        {
            _database.Entries.Add(entry);
            await _database.SaveChangesAsync();
        }

        public async Task RemoveAsync(Entry entry)
        {
            _database.Entries.Remove(entry);
            await _database.SaveChangesAsync();
        }

        public async Task<List<Entry>> GetAllAsync()
        {
            return await _database.Entries
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Entry?> GetByIdAsync(Guid id)
        {
            return await _database.Entries
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Entry entry)
        {
            _database.Entries.Update(entry);
            await _database.SaveChangesAsync();
        }
    }
}
