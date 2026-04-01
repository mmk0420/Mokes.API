using Mokes.API.DTOs;
using Mokes.API.Services;

namespace Mokes.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/register", async (RegisterUserDTO dto, IAuthService service) => 
            {
                if (!MiniValidation.MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);
                var user = await service.Register(dto);
                return Results.Ok(user);
            });

            group.MapPost("/login", async (LoginUserDTO dto, IAuthService service, HttpContext context) => 
            {
                var token = await service.Login(dto);
                if (token == null)
                    return Results.BadRequest("Неверный логин или пароль");

                context.Response.Cookies.Append("dont-play-with-cookie", token);

                return Results.Ok(token);
            });

            group.MapPost("/logout", (HttpContext context) => 
            {
                context.Response.Cookies.Delete("dont-play-with-cookie");
            });

        }
    }
}
