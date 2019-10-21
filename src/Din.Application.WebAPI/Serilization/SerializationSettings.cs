using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Din.Application.WebAPI.Serilization
{
    public static class SerializationSettings
    {
        public static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = GetContractResolver()
            };
        }

        public static IContractResolver GetContractResolver()
        {
            return new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }
    }
}