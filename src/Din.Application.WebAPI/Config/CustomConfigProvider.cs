using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NETCore.Encrypt;

namespace Din.Application.WebAPI.Config
{
    public static class CustoMconfigProviderExtensions
    {
        public static IConfigurationBuilder AddEncryptedProvider(this IConfigurationBuilder builder)
        {
            return builder.Add(new CustoMconfigProvider());
        }
    }

    public class CustoMconfigProvider : ConfigurationProvider, IConfigurationSource
    {
        public override void Load()
        {
            
            Data = UnEncryptMyConfiguration();
        }

        private IDictionary<string, string> UnEncryptMyConfiguration()
        {
            IDictionary<string, string> unEncryptedCollection = new Dictionary<string, string>();
            JObject jObject;

            using (var sr =  new StreamReader($"appsettings.{Environment.GetEnvironmentVariable("ENV")}.json"))
            {
                jObject = JObject.Parse(EncryptProvider.AESDecrypt(sr.ReadToEnd(),
                    Environment.GetEnvironmentVariable("AES_KEY"), Environment.GetEnvironmentVariable("AES_IV")));
            }

            foreach (var property in jObject.Properties())
            {
                foreach (var childProperty in property.Value)
                {
                    var realProp = (JProperty) childProperty;
                    unEncryptedCollection.Add($"{property.Name}:{realProp.Name}", realProp.Value.ToString());
                }
            }
               
            return unEncryptedCollection;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustoMconfigProvider();
        }
    }
}
