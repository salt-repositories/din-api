using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class TokenConfig : ITokenConfig
    {
        public string Issuer { get; }
        public string Key { get; }
        public string ClientId { get; }
        public string Secret { get; }

        public TokenConfig(string issuer, string key, string clientId, string secret)
        {
            Issuer = issuer;
            Key = key;
            ClientId = clientId;
            Secret = secret;
        }
    }
}