using AutoMapper;
using Din.Application.WebAPI.Mapping;
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
                config.AddProfile(new MappingProfile());
            })), Lifestyle.Singleton);
        }
    }
}
