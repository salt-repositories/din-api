using Din.Domain.Clients.Concrete;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Generators.Concrete;
using Din.Domain.Generators.Interfaces;
using Din.Domain.Services.Concrete;
using Din.Domain.Services.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this Container container)
        {
            container.Register<IAuthService, AuthService>();
            container.Register<IMovieService, MovieService>();
            container.Register<ITvShowService, TvShowService>();
            container.Register<IAccountService, AccountService>();
            container.RegisterSingleton<IMediaGenerator, MediaGenerator>();

            container.Register<IIpStackClient, IpStackClient>();
            container.Register<IMovieClient, MovieClient>();
            container.Register<ITvShowClient, TvShowClient>();
            container.RegisterSingleton<IUnsplashClient, UnsplashClient>();
            container.RegisterSingleton<IGiphyClient, GiphyClient>();
        }
    }
}
