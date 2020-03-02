using Din.Application.WebAPI.Context;
using Din.Domain.Context;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Connections.Concrete;
using Din.Infrastructure.DataAccess.Connections.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ContextRegistration
    {
        public static void RegisterContexts(this Container container)
        {
            container.Register<IRequestContext, RequestContext>(Lifestyle.Scoped);
        }

        public static void RegisterDbContext(this Container container, IConfiguration configuration, IHostEnvironment environment)
        {
            var connectionString = environment.EnvironmentName switch
            {
                "Production" => configuration.GetConnectionString("DinContext"),
                "Nightly" => configuration.GetConnectionString("DinNightlyContext"),
                _ => configuration.GetConnectionString("DinDevContext")
            };

            container.Register(() => new DinContext(connectionString), Lifestyle.Scoped);
            container.Register<IHealthCheckConnection>(() => new HealthCheckConnection(connectionString), Lifestyle.Scoped);
        }
    }
}