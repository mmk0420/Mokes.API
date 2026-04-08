using Mokes.API.DTOs.Entry;

namespace Mokes.API.Services.Entry
{
    public interface IEntryService
    {
        Task<List<EntryResponseDto>> GetAllAsync(Guid userId);
        Task<EntryResponseDto?> GetByIdAsync(Guid id, Guid userId);
        Task<List<EntryResponseDto>> GetAllRemovedAsync(Guid userId);
        Task<EntryResponseDto?> GetRemovedByIdAsync(Guid id, Guid userId);
        Task<EntryResponseDto> AddAsync(CreateEntryDto dto, Guid userId);
        Task<EntryResponseDto?> RemoveAsync(Guid id, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<EntryResponseDto?> UpdateAsync(Guid id, UpdateEntryDto dto, Guid userId);
        Task<EntryResponseDto?> ReturnRemovedEntry(Guid id, Guid userId);
        /*Task<EntryResponseDto?> AddTagAsync(Guid id, Guid userId, Guid tagId);*/
    }
}
