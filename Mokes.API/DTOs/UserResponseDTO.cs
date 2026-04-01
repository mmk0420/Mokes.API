using Mokes.API.Models;

namespace Mokes.API.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime AccountCreated { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
