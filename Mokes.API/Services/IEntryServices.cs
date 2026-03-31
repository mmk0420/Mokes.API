using Mokes.API.DTOs;
using Mokes.API.Models;

namespace Mokes.API.Services
{
    public interface IEntryServices
    {
        Task<List<EntryResponseDTO>> GetAllAsync();
        Task<EntryResponseDTO?> GetByIdAsync(Guid id);
        Task<EntryResponseDTO> AddAsync(CreateEntryDTO dto);
        Task<bool> RemoveAsync(Guid id);
        Task<EntryResponseDTO> UpdateAsync(Guid id, UpdateEntryDTO dto);
    }
}
