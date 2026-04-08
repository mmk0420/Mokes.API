using System.ComponentModel.DataAnnotations;

namespace Mokes.API.DTOs.Entry
{
    public record UpdateEntryDto(
        [property: Required, MaxLength(40)]string Name, 
        string Description);
}
