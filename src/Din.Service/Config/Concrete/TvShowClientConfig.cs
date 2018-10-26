using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class TvShowClientConfig : BaseClientConfig, ITvShowClientConfig
    {
        public string SaveLocation { get; }

        public TvShowClientConfig(string url, string key, string saveLocation) : base(url, key)
        {
            SaveLocation = saveLocation;
        }
    }
}
