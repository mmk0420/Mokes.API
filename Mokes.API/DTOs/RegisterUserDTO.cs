using System.ComponentModel.DataAnnotations;

namespace Mokes.API.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
