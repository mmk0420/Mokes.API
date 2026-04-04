using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetAllAsync(Guid userId);
        Task<Entry?> GetByIdAsync(Guid id, Guid userId);
        Task<List<Entry>> GetAllRemovedAsync(Guid userId);
        Task<Entry?> GetRemovedByIdAsync(Guid id, Guid userId);
        Task AddAsync(Entry entry);
        Task DeleteAsync(Entry entry);
        Task UpdateAsync(Entry entry);
    }
}
