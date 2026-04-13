using Mokes.API.DTOs.User;

namespace Mokes.API.Services.Auth
{
    public interface IAuthService
    {
        Task<UserResponseDto?> Register(RegisterUserDto registerUserDto);
        Task<(string authToken, string refreshToken)?> Login(LoginUserDto loginUserDto);
    }
}
