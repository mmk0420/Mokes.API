using Mokes.API.DTOs.Entry;
using Mokes.API.Repositories.Entry;

namespace Mokes.API.Services.Entry
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repository;
        public EntryService(IEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EntryResponseDto>> GetAllRemovedAsync(Guid userId)
        {
            var entries = await _repository.GetAllBelongingToUserRemovedAsync(userId);
            
            return entries
                .Select(e => new EntryResponseDto
                    (
                        e.Id, 
                        e.Name, 
                        e.Description, 
                        e.Created, 
                        e.DeletedAt
                    )
                ).ToList();
        }

        public async Task<EntryResponseDto?> GetRemovedByIdAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return null;

            return new EntryResponseDto
                (
                    entry.Id, 
                    entry.Name, 
                    entry.Description, 
                    entry.Created, 
                    entry.DeletedAt
                );
        }

        public async Task<EntryResponseDto> AddAsync(CreateEntryDto dto, Guid userId)
        {
            var entry = new Models.Entry
            {
                Name = dto.Name,
                Description = dto.Description,
                Created = DateTime.UtcNow,
                UserId = userId,
                DeletedAt = null
            };

            await _repository.AddAsync(entry);

            return new EntryResponseDto
                (
                    entry.Id, 
                    entry.Name, 
                    entry.Description, 
                    entry.Created, 
                    entry.DeletedAt
                );
        }

        public async Task<List<EntryResponseDto>> GetAllAsync(Guid userId)
        {
            var entries = await _repository.GetAllBelongingToUserAsync(userId);

            return entries
                    .Select(e => new EntryResponseDto
                        (
                            e.Id, 
                            e.Name, 
                            e.Description, 
                            e.Created, 
                            e.DeletedAt
                        )
                    ).ToList();
        }

        public async Task<EntryResponseDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            return new EntryResponseDto
            (
                entry.Id, 
                entry.Name, 
                entry.Description, 
                entry.Created, 
                entry.DeletedAt
            );
        }

        public async Task<EntryResponseDto?> RemoveAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            entry.DeletedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(entry);
            
            return new EntryResponseDto
            (
                entry.Id, 
                entry.Name, 
                entry.Description, 
                entry.Created, 
                entry.DeletedAt
            );
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return false;
            
            await _repository.DeleteAsync(entry);
            
            return true;
        }

        public async Task<EntryResponseDto?> UpdateAsync(Guid id, UpdateEntryDto dto, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserAsync(id, userId);
            if (entry == null) return null;

            entry.Name = dto.Name;
            entry.Description = dto.Description;

            await _repository.UpdateAsync(entry);

            return new EntryResponseDto
            (
                entry.Id, 
                entry.Name, 
                entry.Description, 
                entry.Created, 
                entry.DeletedAt
            );
        }

        public async Task<EntryResponseDto?> ReturnRemovedEntry(Guid id, Guid userId)
        {
            var entry = await _repository.GetByIdBelongingToUserRemovedAsync(id, userId);
            if (entry == null) return null;

            entry.DeletedAt = null;
            
            await _repository.UpdateAsync(entry);
            
            return new EntryResponseDto
            (
                entry.Id, 
                entry.Name, 
                entry.Description, 
                entry.Created, 
                entry.DeletedAt
            );
        }

        /*public async Task<EntryResponseDto?> AddTagAsync(Guid id, Guid userId, Guid tagId)
        {
            
        }*/
    }
}
