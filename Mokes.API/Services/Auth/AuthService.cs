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

        public async Task<(string, Guid)?> Login(LoginUserDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null || !_passwordHasher.Verify(dto.Password, user.HashedPassword))
                return null;

            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);
            if (refreshToken == null)
                return null;
            
            var authToken = await _tokenService.AuthTokenRefreshAsyns((Guid)refreshToken);
            if (authToken == null)
                return null;
            
            return (authToken, (Guid)refreshToken);
        }

        public async Task<UserResponseDto?> Register(RegisterUserDto dto)
        {
            var existing = await _repository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return null;

            var newUser = new User
            {
                AccountCreated = DateTime.Now,
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
