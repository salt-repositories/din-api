using Din.Application.WebAPI.Context;
using Din.Domain.Context;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Connections.Concrete;
using Din.Infrastructure.DataAccess.Connections.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ContextRegistration
    {
        public static void RegisterContexts(this Container container)
        {
            container.Register<IRequestContext, RequestContext>(Lifestyle.Scoped);
        }

        public static void RegisterDbContext(this Container container, IConfiguration configuration, IHostingEnvironment environment)
        {
            string connectionString;

            switch (environment.EnvironmentName)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("DinContext");
                    break;
                case "Nightly":
                    connectionString = configuration.GetConnectionString("DinNightlyContext");
                    break;
                default:
                    connectionString = configuration.GetConnectionString("DinDevContext");
                    break;
            }

            container.Register(() => new DinContext(connectionString), Lifestyle.Scoped);
            container.Register<IHealthCheckConnection>(() => new HealthCheckConnection(connectionString), Lifestyle.Scoped);
        }
    }
}