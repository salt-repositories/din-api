using Din.Domain.Clients.Concrete;
using Din.Domain.Clients.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class HttpClients
    {
        public static void RegisterHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IGiphyClient, GiphyClient>();
            services.AddHttpClient<IIpStackClient, IpStackClient>();
            services.AddHttpClient<IMovieClient, MovieClient>();
            services.AddHttpClient<ITvShowClient, TvShowClient>();
            services.AddHttpClient<IUnsplashClient, UnsplashClient>();
        }
    }
}
