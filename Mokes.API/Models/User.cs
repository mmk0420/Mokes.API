namespace Mokes.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public DateTime AccountCreated { get; set; }
        public List<Entry> Entries { get; set; } = new();
    }
}
