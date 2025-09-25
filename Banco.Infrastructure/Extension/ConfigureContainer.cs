using Banco.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Banco.Infrastructure.Extension
{
    /// <summary>
    /// Extension del contenedor de configuracion de servicios
    /// </summary>
    public static class ConfigureContainer
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}