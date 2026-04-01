using Mokes.API.DTOs;
using Mokes.API.Models;
using Mokes.API.Repositories;
using Mokes.API.Utils;

namespace Mokes.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJWTGenerator _jwtGenerator;
        public AuthService(IUserRepository repository, IJWTGenerator jwtGenerator)
        {
            _repository = repository;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<string?> Login(LoginUserDTO dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null || !PasswordHasher.Verify(dto.Password, user.HashedPassword))
                return null;

            var token = _jwtGenerator.GenerateToken(user);

            return token;
        }

        public async Task<UserResponseDTO> Register(RegisterUserDTO dto)
        {
            User newUser = new User
            {
                AccountCreated = DateTime.Now,
                Email = dto.Email,
                Username = dto.Username,
                HashedPassword = PasswordHasher.Hash(dto.Password)
            };

            await _repository.AddAsync(newUser);

            return new UserResponseDTO
            {
                AccountCreated = newUser.AccountCreated,
                Email = newUser.Email,
                Id = newUser.Id,
                Username = newUser.Username
            };
        }
    }
}
