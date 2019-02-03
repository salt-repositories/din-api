using AutoMapper;
using Din.Application.WebAPI.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class AutoMapper
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
