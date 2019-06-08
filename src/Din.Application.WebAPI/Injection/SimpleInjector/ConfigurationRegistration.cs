using Din.Domain.Clients.Giphy.Concrete;
using Din.Domain.Clients.Giphy.Interfaces;
using Din.Domain.Clients.IpStack.Concrete;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Clients.Radarr.Concrete;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Sonarr.Concrete;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Unsplash.Concrete;
using Din.Domain.Clients.Unsplash.Interfaces;
using Din.Domain.Configurations.Concrete;
using Din.Domain.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ConfigurationRegistration
    {
        public static void RegisterConfigurations(this Container container, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();
            var giphyClientConfig = configuration.GetSection("Giphy").Get<GiphyClientConfig>();
            var ipStackClientConfig = configuration.GetSection("IpStack").Get<IpStackClientConfig>();
            var radarrClientConfig = configuration.GetSection("MovieClient").Get<RadarrClientConfig>();
            var sonarrClientConfig = configuration.GetSection("TvShowClient").Get<SonarrClientConfig>();
            var unsplashConfig = configuration.GetSection("Unsplash").Get<UnsplashClientConfig>();

            container.RegisterInstance<IJwtConfig>(jwtConfig);
            container.RegisterInstance<IGiphyClientConfig>(giphyClientConfig);
            container.RegisterInstance<IIpStackClientConfig>(ipStackClientConfig);
            container.RegisterInstance<IRadarrClientConfig>(radarrClientConfig);
            container.RegisterInstance<ISonarrClientConfig>(sonarrClientConfig);
            container.RegisterInstance<IUnsplashClientConfig>(unsplashConfig);
        }
    }
}