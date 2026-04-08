using Mokes.API.DTOs.User;

namespace Mokes.API.Services.Auth
{
    public interface IAuthService
    {
        Task<UserResponseDto?> Register(RegisterUserDto registerUserDTO);
        Task<(string, Guid)?> Login(LoginUserDto loginUserDTO);
    }
}
