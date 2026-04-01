
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mokes.API.DataBase;
using Mokes.API.Endpoints;
using Mokes.API.Models;
using Mokes.API.Repositories;
using Mokes.API.Services;

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

            builder.Services.AddScoped<IEntryServices, EntryServices>();
            builder.Services.AddScoped<IEntryRepository, EntryRepository>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapEntryEndpoints();

            app.Run();
        }
    }
}
