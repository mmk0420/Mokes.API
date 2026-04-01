
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Mokes.API.DataBase;
using Mokes.API.Endpoints;
using Mokes.API.Extensions;
using Mokes.API.Repositories;
using Mokes.API.Services;
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
            builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();

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
