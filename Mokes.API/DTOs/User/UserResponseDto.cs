using Mokes.API.DTOs.Entry;

namespace Mokes.API.DTOs.User
{
    public record UserResponseDto
    (
        Guid Id, 
        string Username, 
        string Email, 
        DateTime AccountCreated, 
        List<EntryResponseDto>? Entries = null);
}
