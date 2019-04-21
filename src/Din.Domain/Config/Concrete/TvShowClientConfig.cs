using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
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
