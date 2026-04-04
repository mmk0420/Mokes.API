namespace Mokes.API.DTOs
{
    public class EntryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description {  get; set; }
        public DateTime Created {  get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
