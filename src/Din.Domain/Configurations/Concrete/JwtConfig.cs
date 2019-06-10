using Din.Domain.Configurations.Interfaces;

namespace Din.Domain.Configurations.Concrete
{
    public class JwtConfig : IJwtConfig
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}
