using Din.Domain.Clients.Concrete;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Services.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this Container container)
        {
//            container.Register<IMovieService, MovieService>();
//            container.Register<ITvShowService, TvShowService>();
//            container.RegisterSingleton<IMediaGenerator, MediaGenerator>();
        }
    }
}
