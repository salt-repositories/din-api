using Din.Domain.Config.Concrete;
using Din.Domain.Config.Interfaces;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class Configurations
    {
        public static void RegisterConfigurations(this Container container, IConfiguration configuration)
        {
            container.RegisterInstance<ITokenConfig>(new TokenConfig(configuration["Jwt:Issuer"],
                configuration["Jwt:Key"]));
            container.RegisterInstance<IGiphyClientConfig>(new GiphyClientConfig(configuration["GiphyClient:Url"],
                configuration["GiphyClient:Key"]));
            container.RegisterInstance<IIpStackClientConfig>(new IpStackClientConfig(configuration["IpStackClient:Url"],
                configuration["IpStackClient:Key"]));
            container.RegisterInstance<IMovieClientConfig>(new MovieClientConfig(configuration["MovieClient:Url"],
                configuration["MovieClient:Key"], configuration["MovieClient:SaveLocation"]));
            container.RegisterInstance<ITvShowClientConfig>(new TvShowClientConfig(configuration["TvShowClient:Url"],
                configuration["TvShowClient:Key"], configuration["TvShowClient:SaveLocation"]));
            container.RegisterInstance<IUnsplashClientConfig>(new UnsplashClientConfig(configuration["UnsplashClient:Url"],
                configuration["UnsplashClient:Key"]));
            container.RegisterInstance<ITMDBClientConfig>(new TMDBClientConfig(configuration["TMDBClient:Key"]));
        }
    }
}
