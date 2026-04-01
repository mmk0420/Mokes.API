namespace Mokes.API.Utils
{
    public class PasswordHasher
    {
        public static string Hash(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public static bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
