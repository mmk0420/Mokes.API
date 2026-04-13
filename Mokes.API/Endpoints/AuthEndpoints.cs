using Mokes.API.DTOs.User;
using Mokes.API.Services.Auth;

namespace Mokes.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/register", async (RegisterUserDto dto, IAuthService service) => 
            {
                if (!MiniValidation.MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);
                var user = await service.Register(dto);
                return user == null ? Results.Conflict("Пользователь с таким именем уже существует") : Results.Ok(user);
            });

            group.MapPost("/login", async (
                LoginUserDto dto, 
                IAuthService authService, 
                HttpContext context, 
                IConfiguration config) =>
            {
                var tokens = await authService.Login(dto);
                if (!tokens.IsSuccess)
                {
                    switch (tokens.StatusCode)
                    {
                        case 401:
                            return Results.Unauthorized();
                        case 502:
                            return Results.BadRequest(tokens.Error);
                    }
                }
                
                var authToken = tokens.Value.Item1;
                var refreshToken = tokens.Value.Item2;

                context.Response.Cookies.Append(config["Cookie:Auth"]!, authToken);
                context.Response.Cookies.Append(config["Cookie:Refresh"]!, refreshToken);

                return Results.Ok();
            });

            group.MapPost("/logout", (HttpContext context, IConfiguration config) => 
            {
                context.Response.Cookies.Delete(config["Cookie:Auth"]!);
                context.Response.Cookies.Delete(config["Cookie:Refresh"]!);
            });

            group.MapPost("/refresh", async (ITokenService tokenService, HttpContext context, IConfiguration config) =>
            {
                var refreshToken = context.Request.Cookies[config["Cookie:Refresh"]!];
                if  (refreshToken == null)
                    return Results.Unauthorized();
                var userId = Guid.Parse(context.User.FindFirst("userId").Value);
                
                var newAuthToken = await tokenService.AuthTokenRefreshAsync(refreshToken);
                if (!newAuthToken.IsSuccess)
                {
                    if (newAuthToken.StatusCode == 502)
                    {
                        return Results.BadRequest(newAuthToken.Error);
                    }
                }
                var newRefreshToken = await tokenService.GenerateRefreshTokenAsync(userId);
                
                context.Response.Cookies.Delete(config["Cookie:Refresh"]!);
                context.Response.Cookies.Append(config["Cookie:Refresh"]!, newRefreshToken);
                context.Response.Cookies.Delete(config["Cookie:Auth"]!);
                context.Response.Cookies.Append(config["Cookie:Auth"]!, newAuthToken.Value!);
                
                return Results.Ok();
            });
        }
    }
}
