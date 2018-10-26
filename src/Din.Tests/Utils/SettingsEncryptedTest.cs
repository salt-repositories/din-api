using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NETCore.Encrypt;
using Xunit;

namespace Din.Tests.Utils
{
    public class SettingsEncryptedTest
    {
        [Fact]
        public void AppSettingsIsSecureTest()
        {
            var files = Directory.GetFiles(@"./", "appsettings*.json");
            foreach (var f in files)
            {
                using (var sr = new StreamReader(f))
                {
                    bool secure;
                    try
                    {
                        JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        secure = false;
                        Assert.True(secure);
                    }
                    catch (JsonException)
                    {
                        try
                        {
                            var aesKey = EncryptProvider.CreateAesKey();
                            EncryptProvider.AESDecrypt(sr.ReadToEnd(), aesKey.Key, aesKey.IV);
                            secure = false;
                            Assert.True(secure);
                        }
                        catch (ArgumentException)
                        {
                            secure = true;
                            Assert.True(secure);
                        }
                    }
                }
            }
        }
    }
}
