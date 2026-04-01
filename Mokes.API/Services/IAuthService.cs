using Mokes.API.DTOs;

namespace Mokes.API.Services
{
    public interface IAuthService
    {
        Task<UserResponseDTO> Register(RegisterUserDTO registerUserDTO);
        Task<string?> Login(LoginUserDTO loginUserDTO);
    }
}
