using Mokes.API.Models;
using Mokes.API.Repositories.Token;
using Mokes.API.Utils;

namespace Mokes.API.Services.Auth;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _repository;
    private readonly IJwtHelper _jwtHelper;
    public TokenService(ITokenRepository repository, IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
        _repository = repository;
    }
    public async Task<string?> AuthTokenRefreshAsyns(Guid tokenIdentifier)
    {
        var token = await _repository.GetByIdentifierAsync(tokenIdentifier);
        if (token == null)
            return null;
        if (!token.IsActive || token.Expires <= DateTime.UtcNow) 
            return null;
        
        var authToken = _jwtHelper.GenerateAuthToken(token.UserId);
        return authToken;
    }

    public async Task<Guid?> GenerateRefreshTokenAsync(Guid userId)
    {
        var token = new RefreshToken
        {
            Token = Guid.NewGuid(),
            Expires = DateTime.UtcNow.AddDays(7),
            IsActive = true,
            UserId = userId
        };
        
        await _repository.AddAsync(token);

        return token.Token;
    }
}