using Mokes.API.Models;

namespace Mokes.API.Repositories;

public interface ITagRepository
{
    Task<List<Tag>> GetAllBelongingToUserAsync(Guid userId);
    Task<Tag?> GetByIdBelongingToUserAsync(Guid id, Guid userId);
    Task<List<Tag>> GetAllBelongingToUserRemovedAsync(Guid userId);
    Task<Tag?> GetByIdBelongingToUserRemovedAsync(Guid id, Guid userId);
    Task AddAsync(Tag tag);
    Task DeleteAsync(Tag tag);
    Task UpdateAsync(Tag tag);
}