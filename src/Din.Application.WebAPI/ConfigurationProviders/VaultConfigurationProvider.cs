using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods.GitHub;
using VaultSharp.V1.Commons;

namespace Din.Application.WebAPI.ConfigurationProviders
{
    public static class ConfigurationExtensions
    {
        public static IHostingEnvironment Environment;

        public static void AddVaultSecrets(this IConfigurationBuilder builder, IHostingEnvironment environment)
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
            using (var loader = new VaultSecretLoader())
            {
                Data = loader.GetVaultSecrets().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        private sealed class VaultSecretLoader : IDisposable
        {
            private bool _disposed;
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
                _vaultKeyList = await _client.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync("din-api");

                _secretDataDictionary = new Dictionary<string, Secret<SecretData>>();

                foreach (var key in _vaultKeyList.Data.Keys)
                {
                    _secretDataDictionary.Add(key,
                        await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync($"din-api/{key}"));
                }

                _configuration = new Dictionary<string, string>();

                foreach (var data in _secretDataDictionary)
                {
                    foreach (var secret in data.Value.Data.Data)
                    {
                        _configuration.Add($"{data.Key}:{secret.Key}", $"{secret.Value}");
                    }
                }

                return _configuration;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }

                if (disposing)
                {
                    _vaultKeyList.Data.Keys.GetEnumerator().Dispose();
                    _secretDataDictionary.GetEnumerator().Dispose();
                    _configuration.GetEnumerator().Dispose();
                }

                _disposed = true;
            }
        }
    }
}