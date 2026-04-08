using System.ComponentModel.DataAnnotations;

namespace Mokes.API.DTOs.User
{
    public record RegisterUserDto(
        [property: Required, MaxLength(30), MinLength(3)] string Username, 
        [property: Required, MinLength(10), MaxLength(100)] string Password,
        [property: Required, EmailAddress] string Email);
}
