using AutoMapper;
using Din.Application.WebAPI.Accounts.Mapping;
using Din.Application.WebAPI.Media.Mapping;
using Din.Application.WebAPI.Movies.Mapping;
using Din.Application.WebAPI.TvShows.Mapping;
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
