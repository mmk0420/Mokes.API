using Mokes.API.Models;

namespace Mokes.API.Services.Auth;

public interface ITokenService
{
    Task<string?> AuthTokenRefreshAsync(string tokenIdentifier);
    Task<string?> GenerateRefreshTokenAsync(Guid userId);
}