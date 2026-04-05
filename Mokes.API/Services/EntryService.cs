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

        public async Task<List<EntryResponseDTO>> GetAllRemovedAsync(Guid userId)
        {
            var entries = await _repository.GetAllBelongingToUserRemovedAsync(userId);
            
            return entries
                .Select(e => new EntryResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Created = e.Created,
                    DeletedAt = e.DeletedAt
                }).ToList();
        }

        public async Task<EntryResponseDTO?> GetRemovedByIdAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return null;

            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                DeletedAt = entry.DeletedAt
            };
        }

        public async Task<EntryResponseDTO> AddAsync(CreateEntryDTO dto, Guid userId)
        {
            Entry entry = new Entry
            {
                Name = dto.Name,
                Description = dto.Description,
                Created = DateTime.UtcNow,
                UserId = userId,
                DeletedAt =  null
            };

            await _repository.AddAsync(entry);

            return new EntryResponseDTO
            {
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                Id = entry.Id,
                DeletedAt = entry.DeletedAt
            };
        }

        public async Task<List<EntryResponseDTO>> GetAllAsync(Guid userId)
        {
            var entries = await _repository.GetAllBelongingToUserAsync(userId);

            return entries
                    .Select(e => new EntryResponseDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Created = e.Created,
                        DeletedAt =  e.DeletedAt
                    }).ToList();
        }

        public async Task<EntryResponseDTO?> GetByIdAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                DeletedAt = entry.DeletedAt
            };
        }

        public async Task<EntryResponseDTO?> RemoveAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            entry.DeletedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(entry);
            
            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                DeletedAt = entry.DeletedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return false;
            
            await _repository.DeleteAsync(entry);
            
            return true;
        }

        public async Task<EntryResponseDTO?> UpdateAsync(Guid id, UpdateEntryDTO dto, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            entry.Name = dto.Name;
            entry.Description = dto.Description;

            await _repository.UpdateAsync(entry);

            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                DeletedAt = entry.DeletedAt
            };
        }

        public async Task<EntryResponseDTO?> ReturnEntry(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return null;

            entry.DeletedAt = null;
            
            await _repository.UpdateAsync(entry);
            
            return new EntryResponseDTO
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description,
                Created = entry.Created,
                DeletedAt = entry.DeletedAt
            };
        }
    }
}
