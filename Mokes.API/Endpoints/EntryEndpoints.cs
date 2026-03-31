using Mokes.API.DataBase;
using System.Runtime.CompilerServices;

namespace Mokes.API.Endpoints
{
    public static class EntryEndpoints
    {
        public static void MapEntryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/entries");

            group.MapGet("/", () => 
            {
                
            });
        }
    }
}
