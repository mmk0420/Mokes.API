namespace Mokes.API.Models
{
    public class Entry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
