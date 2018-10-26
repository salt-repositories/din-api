using Din.Service.Clients.Interfaces;
using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class TMDBClientConfig : BaseClientConfig, ITMDBClientConfig
    {
        public TMDBClientConfig(string key) : base(null, key)
        {

        }
    }
}
