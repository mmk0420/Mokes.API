using Mokes.API.Models;

namespace Mokes.API.Repositories
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetAllBelongingToUserAsync(Guid userId);
        Task<Entry?> GetByIdBelongingToUserAsync(Guid id, Guid userId);
        Task<List<Entry>> GetAllBelongingToUserRemovedAsync(Guid userId);
        Task<Entry?> GetByIdBelongingToUserRemovedAsync(Guid id, Guid userId);
        Task AddAsync(Entry entry);
        Task DeleteAsync(Entry entry);
        Task UpdateAsync(Entry entry);
    }
}
