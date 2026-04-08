namespace Mokes.API.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}