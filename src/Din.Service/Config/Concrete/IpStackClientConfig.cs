using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class IpStackClientConfig : BaseClientConfig, IIpStackClientConfig
    {
        public IpStackClientConfig(string url, string key) : base(url, key)
        {

        }
    }
}
