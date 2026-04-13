using System.Security.Cryptography;
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
    public async Task<Result<string>> AuthTokenRefreshAsync(string tokenIdentifier)
    {
        var refreshToken = await _repository.GetByIdentifierAsync(tokenIdentifier);
        if (refreshToken == null)
            return new Result<string>
            {
                Error = "Токен не найден",
                StatusCode = 502
            };
        if (!refreshToken.IsActive || refreshToken.Expires <= DateTime.UtcNow)
            return new Result<string>
            {
                Error = "Токен недействителен",
                StatusCode = 502
            };
        
        refreshToken.IsActive = false;
        var authToken = _jwtHelper.GenerateAuthToken(refreshToken.UserId);
        return new Result<string>
        {
            StatusCode = 200,
            Value = authToken
        };
    }

    public async Task<string> GenerateRefreshTokenAsync(Guid userId)
    {
        var token = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            IsActive = true,
            UserId = userId
        };
        
        await _repository.AddAsync(token);

        return token.Token;
    }
}