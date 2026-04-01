using Microsoft.IdentityModel.Tokens;
using Mokes.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Mokes.API.Utils
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _configuration;
        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken
                (expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials,
                claims: claims);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
