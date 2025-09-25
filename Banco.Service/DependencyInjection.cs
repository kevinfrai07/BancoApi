using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Banco.Service
{
    public static class DependencyInjection
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

    }
}

