namespace Mokes.API.Utils;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashPassword);
}