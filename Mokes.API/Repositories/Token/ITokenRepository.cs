using Mokes.API.Models;

namespace Mokes.API.Repositories.Token;

public interface ITokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByIdAsync(Guid tokenId);
    Task<RefreshToken?> GetByIdentifierAsync(Guid tokenIdentifier);
}