using System.ComponentModel.DataAnnotations;

namespace Mokes.API.DTOs.User
{
    public record LoginUserDto(
        [property: Required] string Username, 
        [property: Required] string Password);
}
