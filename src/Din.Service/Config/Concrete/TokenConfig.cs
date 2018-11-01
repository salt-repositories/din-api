using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class TokenConfig : ITokenConfig
    {
        public string Issuer { get; }
        public string Key { get; }

        public TokenConfig(string issuer, string key)
        {
            Issuer = issuer;
            Key = key;
        }
    }
}