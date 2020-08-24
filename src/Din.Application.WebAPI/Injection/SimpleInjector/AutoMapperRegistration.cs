using System.Reflection;
using AutoMapper;
using Din.Application.WebAPI.Accounts.Mapping;
using Din.Application.WebAPI.Authentication.Mapping;
using Din.Application.WebAPI.Content.Mapping;
using Din.Application.WebAPI.Media.Mapping;
using Din.Application.WebAPI.Movies.Mapping;
using Din.Application.WebAPI.TvShows.Mapping;
using Din.Domain.Extensions;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class AutoMapperRegistration
    {
        public static void RegisterAutoMapper(this Container container, Assembly[] assemblies)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(container.GetInstance);
                config.Advanced.AllowAdditiveTypeMapCreation = true;
                config.AllowNullCollections = true;
                config.AddProfile(new AuthenticationMappingProfile());
                config.AddProfile(new AccountsMappingProfile());
                config.AddProfile(new MovieMappingProfile());
                config.AddProfile(new TvShowMappingProfile());
                config.AddProfile(new MediaMappingProfile());
                config.AddProfile(new ContentMappingProfile());
            });
            
            container.Register<IMapper>(() => new Mapper(configuration), Lifestyle.Singleton);
            
            foreach (var resolver in assemblies.GetGenericInterfaceImplementationTypes(typeof(IValueResolver<,,>)))
            {
                container.Register(resolver);
            }

            foreach (var converter in assemblies.GetGenericInterfaceImplementationTypes(typeof(ITypeConverter<,>)))
            {
                container.Register(converter);
            }
        }
    }
}
