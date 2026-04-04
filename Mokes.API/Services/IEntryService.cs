using Mokes.API.DTOs;
using Mokes.API.Models;

namespace Mokes.API.Services
{
    public interface IEntryService
    {
        Task<List<EntryResponseDTO>> GetAllAsync(Guid userId);
        Task<EntryResponseDTO?> GetByIdAsync(Guid id, Guid userId);
        Task<List<EntryResponseDTO>> GetAllRemovedAsync(Guid userId);
        Task<EntryResponseDTO?> GetRemovedByIdAsync(Guid id, Guid userId);
        Task<EntryResponseDTO> AddAsync(CreateEntryDTO dto, Guid userId);
        Task<EntryResponseDTO?> RemoveAsync(Guid id, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<EntryResponseDTO?> UpdateAsync(Guid id, UpdateEntryDTO dto, Guid userId);
        Task<EntryResponseDTO?> ReturnEntry(Guid id, Guid userId);
    }
}
