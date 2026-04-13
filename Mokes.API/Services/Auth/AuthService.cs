using Mokes.API.DTOs.User;
using Mokes.API.Models;
using Mokes.API.Repositories.User;
using Mokes.API.Utils;

namespace Mokes.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository repository, IJwtHelper jwtHelper, IPasswordHasher passwordHasher,  ITokenService tokenService)
        {
            _tokenService = tokenService;
            _repository = repository;
            _jwtHelper = jwtHelper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<(string authToken, string refreshToken)>> Login(LoginUserDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null || !_passwordHasher.Verify(dto.Password, user.HashedPassword))
            {
                return new Result<(string authToken, string refreshToken)>
                {
                    Error = "Неверный логин или пароль",
                    StatusCode = 401,
                };
            }
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);
            
            var authToken = await _tokenService.AuthTokenRefreshAsync(refreshToken);
            if (!authToken.IsSuccess)
                return new Result<(string authToken, string refreshToken)>
                {
                    Error = authToken.Error,
                    StatusCode = authToken.StatusCode,
                };

            return new Result<(string authToken, string refreshToken)>
            {
                StatusCode = authToken.StatusCode,
                Value = (authToken.Value, refreshToken)!
            };
        }

        public async Task<UserResponseDto?> Register(RegisterUserDto dto)
        {
            var existing = await _repository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return null;

            var newUser = new User
            {
                AccountCreated = DateTime.UtcNow,
                Email = dto.Email,
                Username = dto.Username,
                HashedPassword = _passwordHasher.Hash(dto.Password)
            };

            await _repository.AddAsync(newUser);

            return new UserResponseDto(
                newUser.Id,
                newUser.Username,
                newUser.Email,
                newUser.AccountCreated);
        }
    }
}
