using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Models;

namespace Mokes.API.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _database;
    public TagRepository(AppDbContext database)
    {
        _database = database;
    }
    
    public async Task<List<Tag>> GetAllBelongingToUserAsync(Guid userId)
    {
        return await _database.Tags
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<Tag?> GetByIdBelongingToUserAsync(Guid id, Guid userId)
    {
        return await  _database.Tags
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Tag>> GetAllBelongingToUserRemovedAsync(Guid userId)
    {
        return await  _database.Tags
            .AsNoTracking()
            .Where(t => t.UserId == userId && t.DeletedAt != null)
            .ToListAsync();
    }

    public async Task<Tag?> GetByIdBelongingToUserRemovedAsync(Guid id, Guid userId)
    {
        return await   _database.Tags
            .AsNoTracking()
            .Where(t => t.UserId == userId && t.DeletedAt != null)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Tag tag)
    {
        _database.Tags.Add(tag);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _database.Tags.Remove(tag);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _database.Tags.Update(tag);
        await _database.SaveChangesAsync();
    }
}