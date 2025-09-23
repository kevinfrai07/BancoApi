using Microsoft.AspNetCore.Builder;
using Banco.Infraestructure.Middleware;

namespace Banco.Infraestructure.Extension
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