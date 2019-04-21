using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
{
    public class IpStackClientConfig : BaseClientConfig, IIpStackClientConfig
    {
        public IpStackClientConfig(string url, string key) : base(url, key)
        {

        }
    }
}
