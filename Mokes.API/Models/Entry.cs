namespace Mokes.API.Models
{
    public class Entry
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
