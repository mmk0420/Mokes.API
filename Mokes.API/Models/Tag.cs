namespace Mokes.API.Models;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Entry> Entries { get; set; }
}