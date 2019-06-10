using AutoMapper;
using Din.Application.WebAPI.Mapping.Accounts;
using Din.Application.WebAPI.Mapping.Media;
using Din.Application.WebAPI.Mapping.Movies;
using Din.Application.WebAPI.Mapping.TvShows;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class AutoMapperRegistration
    {
        public static void RegisterAutoMapper(this Container container)
        {
            container.Register<IMapper>(() => new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMissingTypeMaps = true;
                config.AddProfile(new AccountsMappingProfile());
                config.AddProfile(new MovieMappingProfile());
                config.AddProfile(new TvShowMappingProfile());
                config.AddProfile(new MediaMappingProfile());
            })), Lifestyle.Singleton);
        }
    }
}
