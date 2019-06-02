using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class MvcComponents
    {
        public static void RegisterMvcComponents(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                options.SerializerSettings.ContractResolver = contractResolver;
                options.SerializerSettings.Formatting = Formatting.Indented;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());
        }
    }
}
