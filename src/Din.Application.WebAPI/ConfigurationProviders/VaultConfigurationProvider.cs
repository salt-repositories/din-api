using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods.GitHub;
using VaultSharp.V1.Commons;

namespace Din.Application.WebAPI.ConfigurationProviders
{
    public static class ConfigurationExtensions
    {
        public static IHostEnvironment Environment;

        public static void AddVaultSecrets(this IConfigurationBuilder builder, IHostEnvironment environment)
        {
            Environment = environment;
            builder.Add(new VaultConfigurationProvider());
        }
    }

    public class VaultConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider();
        }

        public override void Load()
        {
            Data = new VaultSecretLoader().GetVaultSecrets().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private sealed class VaultSecretLoader
        {
            private readonly IVaultClient _client;

            private Secret<ListInfo> _vaultKeyList;
            private Dictionary<string, Secret<SecretData>> _secretDataDictionary;
            private Dictionary<string, string> _configuration;

            public VaultSecretLoader()
            {
                var authMethod = ConfigurationExtensions.Environment.IsDevelopment()
                    ? new GitHubAuthMethodInfo(Environment.GetEnvironmentVariable("VAULT_GITHUB_AT"))
                    : new AppRoleAuthMethodInfo(Environment.GetEnvironmentVariable("VAULT_ID"),
                        Environment.GetEnvironmentVariable("VAULT_SECRET")) as IAuthMethodInfo;

                var vaultClientSettings =
                    new VaultClientSettings(Environment.GetEnvironmentVariable("VAULT_URL"), authMethod);

                _client = new VaultClient(vaultClientSettings);
            }

            public async Task<Dictionary<string, string>> GetVaultSecrets()
            {
                try
                {
                    _vaultKeyList = await _client.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync("din-api", "secret");

                    _secretDataDictionary = new Dictionary<string, Secret<SecretData>>();

                    foreach (var key in _vaultKeyList.Data.Keys)
                    {
                        _secretDataDictionary.Add(
                            key,
                            await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync($"din-api/{key}", null, "secret")
                        );
                    }

                    _configuration = new Dictionary<string, string>();

                    foreach (var (key, value) in _secretDataDictionary)
                    {
                        foreach (var (secretName, secretValue) in value.Data.Data)
                        {
                            _configuration.Add($"{key}:{secretName}", $"{secretValue}");
                        }
                    }

                    return _configuration;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}