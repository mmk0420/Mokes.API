using Mokes.API.DTOs;
using Mokes.API.Models;
using Mokes.API.Repositories;

namespace Mokes.API.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repository;
        public EntryService(IEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<EntryResponseDTO> AddAsync(CreateEntryDTO dto, Guid userId)
        {
            Entry entry = new Entry
            {
                Name = dto.Name,
                Description = dto.Description,
                Created = DateTime.Now,
                UserId = userId
            };

            await _repository.AddAsync(entry);

            return new EntryResponseDTO
            {
                Name = entry.Name,
                Decription = entry.Description,
                Created = entry.Created,
                Id = entry.Id
            };
        }

        public async Task<List<EntryResponseDTO>> GetAllAsync(Guid userId)
        {
            var entries = await _repository.GetAllAsync(userId);

            return entries.Select(e => new EntryResponseDTO
            {
                Id = e.Id,
                Name = e.Name,
                Decription = e.Description,
                Created = e.Created
            }).ToList();
        }

        public async Task<EntryResponseDTO?> GetByIdAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdAsync(id, userId);
            if (entry == null) return null;

            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Decription = entry.Description,
                Created = entry.Created
            };
        }

        public async Task<bool> RemoveAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdAsync(id, userId);
            if (entry == null) return false;

            await _repository.RemoveAsync(entry);
            return true;
        }

        public async Task<EntryResponseDTO?> UpdateAsync(Guid id, UpdateEntryDTO dto, Guid userId)
        {
            var originalEntry = await _repository.GetByIdAsync(id, userId);
            if (originalEntry == null) return null;

            originalEntry.Name = dto.Name;
            originalEntry.Description = dto.Description;

            await _repository.UpdateAsync(originalEntry);

            return new EntryResponseDTO
            {
                Id = originalEntry.Id,
                Name = originalEntry.Name,
                Decription = originalEntry.Description,
                Created = originalEntry.Created
            };
        }
    }
}
