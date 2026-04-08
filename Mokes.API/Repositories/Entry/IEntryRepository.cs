namespace Mokes.API.Repositories.Entry
{
    public interface IEntryRepository
    {
        Task<List<Models.Entry>> GetAllBelongingToUserAsync(Guid userId);
        Task<Models.Entry?> GetByIdBelongingToUserAsync(Guid id, Guid userId);
        Task<List<Models.Entry>> GetAllBelongingToUserRemovedAsync(Guid userId);
        Task<Models.Entry?> GetByIdBelongingToUserRemovedAsync(Guid id, Guid userId);
        Task AddAsync(Models.Entry entry);
        Task DeleteAsync(Models.Entry entry);
        Task UpdateAsync(Models.Entry entry);
    }
}
