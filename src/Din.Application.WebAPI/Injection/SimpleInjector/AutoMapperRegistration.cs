using AutoMapper;
using Din.Application.WebAPI.Accounts.Mapping;
using Din.Application.WebAPI.Authentication.Mapping;
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
            var configuration = new MapperConfiguration(config =>
            {
                config.Advanced.AllowAdditiveTypeMapCreation = true;
                config.AllowNullCollections = true;
                config.AddProfile(new AuthenticationMappingProfile());
                config.AddProfile(new AccountsMappingProfile());
                config.AddProfile(new MovieMappingProfile());
                config.AddProfile(new TvShowMappingProfile());
                config.AddProfile(new MediaMappingProfile());
            });
            
            container.Register<IMapper>(() => new Mapper(configuration), Lifestyle.Singleton);
        }
    }
}
