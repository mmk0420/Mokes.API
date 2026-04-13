using Microsoft.EntityFrameworkCore;
using Mokes.API.Data;
using Mokes.API.Models;

namespace Mokes.API.Repositories.Token;

public class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _database;
    public TokenRepository(AppDbContext database)
    {
        _database = database;
    }
    public async Task AddAsync(RefreshToken token)
    {
        _database.RefreshTokens.Add(token);
        await _database.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid tokenId)
    {
        return await _database.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tokenId);
    }

    public async Task<RefreshToken?> GetByIdentifierAsync(string tokenIdentifier)
    {
        return await _database.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Token == tokenIdentifier);
    }
}