using Mokes.API.Models;

namespace Mokes.API.Utils
{
    public interface IJwtHelper
    {
        string GenerateAuthToken(Guid userId);
    }
}