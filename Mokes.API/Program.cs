
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Mokes.API.Data;
using Mokes.API.Endpoints;
using Mokes.API.Extensions;
using Mokes.API.Repositories;
using Mokes.API.Repositories.Entry;
using Mokes.API.Repositories.Token;
using Mokes.API.Repositories.User;
using Mokes.API.Services;
using Mokes.API.Services.Auth;
using Mokes.API.Services.Entry;
using Mokes.API.Utils;

namespace Mokes.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Data/save.db"));

            builder.Services.AddScoped<IEntryService, EntryService>();
            builder.Services.AddScoped<IEntryRepository, EntryRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtHelper, JwtHelper>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();

            builder.Configuration.AddJsonFile("secret.json", optional: true);

            builder.Services.AddApiAuthentication(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapEntryEndpoints();
            app.MapAuthEndpoints();

            app.Run();
        }
    }
}
