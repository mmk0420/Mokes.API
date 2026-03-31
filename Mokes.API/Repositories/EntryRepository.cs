using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _db;
        public EntryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Entry entry)
        {
            _db.Entries.Add(entry);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(Entry entry)
        {
            _db.Entries.Remove(entry);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Entry>> GetAllAsync()
        {
            return await _db.Entries.ToListAsync();
        }

        public async Task<Entry?> GetByIdAsync(Guid id)
        {
            return await _db.Entries.FindAsync(id);
        }

        public async Task UpdateAsync(Entry entry)
        {
            _db.Entries.Update(entry);
            await _db.SaveChangesAsync();
        }
    }
}
