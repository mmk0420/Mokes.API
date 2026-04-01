using Mokes.API.DataBase;
using Mokes.API.DTOs;
using Mokes.API.Services;
using System.Runtime.CompilerServices;
using MiniValidation;

namespace Mokes.API.Endpoints
{
    public static class EntryEndpoints
    {
        public static void MapEntryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/entries");

            group.MapGet("/", async (IEntryService service) => 
                Results.Ok(await service.GetAllAsync()));

            group.MapGet("/{id}", async(IEntryService service, Guid id) => 
            {
                var entry = service.GetByIdAsync(id);
                if (entry == null)
                    return Results.NotFound();
                return Results.Ok(entry);
            });

            group.MapPost("/", async (IEntryService service, CreateEntryDTO dto) => 
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);
                var newEntry = await service.AddAsync(dto);
                return Results.Ok(newEntry);
            });

            group.MapDelete("/{id}", async (IEntryService service, Guid id) =>
            {
                if (!await service.RemoveAsync(id))
                    return Results.NotFound();
                return Results.Ok();
            });

            group.MapPut("/{id}", async(IEntryService service, Guid id, UpdateEntryDTO dto) => 
            {
                var entry = await service.UpdateAsync(id, dto);
                if (entry == null)
                    return Results.NotFound();
                return Results.Ok(entry);
            });
        }
    }
}
