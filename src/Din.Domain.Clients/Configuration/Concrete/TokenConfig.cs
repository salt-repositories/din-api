using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Configuration.Interfaces;

namespace Din.Domain.Clients.Configuration.Concrete
{
    public class TokenConfig : BaseClientConfig, ITokenConfig
    {
        public string Issuer { get; set; }
    }
}