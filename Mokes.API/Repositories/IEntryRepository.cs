using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetAllAsync();
        Task<Entry?> GetByIdAsync(Guid id);
        Task AddAsync(Entry entry);
        Task RemoveAsync(Entry entry);
        Task UpdateAsync(Entry entry);
    }
}
