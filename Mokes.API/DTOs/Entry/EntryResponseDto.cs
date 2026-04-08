namespace Mokes.API.DTOs.Entry
{
    public record EntryResponseDto(
        Guid Id,
        string Name,
        string Description,
        DateTime Created,
        DateTime? DeletedAt);
}
