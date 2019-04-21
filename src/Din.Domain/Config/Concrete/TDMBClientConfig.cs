using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
{
    public class TMDBClientConfig : BaseClientConfig, ITMDBClientConfig
    {
        public TMDBClientConfig(string key) : base(null, key)
        {

        }
    }
}
