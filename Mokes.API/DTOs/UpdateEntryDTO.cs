using System.ComponentModel.DataAnnotations;

namespace Mokes.API.DTOs
{
    public class UpdateEntryDTO
    {
        [Required]
        [MaxLength(21)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
