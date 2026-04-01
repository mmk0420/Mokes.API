using Mokes.API.Models;

namespace Mokes.API.Utils
{
    public interface IJWTGenerator
    {
        string GenerateToken(User user);
    }
}