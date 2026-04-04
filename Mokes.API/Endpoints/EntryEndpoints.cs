using Microsoft.EntityFrameworkCore;
using MiniValidation;
using Mokes.API.DTOs;
using Mokes.API.Services;

namespace Mokes.API.Endpoints
{
    public static class EntryEndpoints
    {
        public static void MapEntryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/entries")
                .RequireAuthorization();

            group.MapGet("/", async (IEntryService service, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                return Results.Ok(await service.GetAllAsync(userId));
            });

            group.MapGet("/{id}", async(IEntryService service, Guid id, HttpContext context) => 
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var entry = await service.GetByIdAsync(id, userId);
                return entry == null ? Results.NotFound() : Results.Ok(entry);
            });

            group.MapGet("/removed", async (IEntryService service, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                return Results.Ok(await service.GetAllRemovedAsync(userId));
            });

            group.MapGet("/removed/{id}", async (IEntryService service, Guid id, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var entry = await service.GetRemovedByIdAsync(id, userId);
                return entry == null ? Results.NotFound() : Results.Ok(entry);
            });

            group.MapPost("/", async (IEntryService service, CreateEntryDTO dto, HttpContext context) => 
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var newEntry = await service.AddAsync(dto, userId);
                return Results.Ok(newEntry);
            });

            group.MapPost("/removed/{id}", async (IEntryService service, Guid id, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var entry = await service.ReturnEntry(id, userId);
                return entry == null ? Results.NotFound() : Results.Ok(entry);
            });

            group.MapDelete("/{id}", async (IEntryService service, Guid id, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var entry =  await service.RemoveAsync(id, userId);
                return entry == null ? Results.Ok(entry) : Results.NotFound();
            });

            group.MapDelete("/removed/{id}", async (IEntryService service, Guid id, HttpContext context) =>
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                return await service.DeleteAsync(id, userId) ? Results.Ok() : Results.NotFound();
            });

            group.MapPut("/{id}", async(IEntryService service, Guid id, UpdateEntryDTO dto, HttpContext context) => 
            {
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                var entry = await service.UpdateAsync(id, dto, userId);
                return entry == null ? Results.NotFound() : Results.Ok(entry);
            });
        }
    }
}
