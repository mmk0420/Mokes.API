namespace Mokes.API.Models;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<Entry> Entries { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}