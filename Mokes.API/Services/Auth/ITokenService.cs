using Mokes.API.Models;

namespace Mokes.API.Services.Auth;

public interface ITokenService
{
    Task<string?> AuthTokenRefreshAsyns(Guid tokenIdentifier);
    Task<Guid?> GenerateRefreshTokenAsync(Guid userId);
}