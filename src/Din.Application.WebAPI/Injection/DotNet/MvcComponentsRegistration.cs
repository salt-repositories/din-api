using Din.Application.WebAPI.Serilization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class MvcComponentsRegistration
    {
        public static void RegisterMvcComponents(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ContractResolver = SerializationSettings.GetContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
            });

            services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());
        }
    }
}
