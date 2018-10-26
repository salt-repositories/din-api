using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class MovieClientConfig : BaseClientConfig, IMovieClientConfig
    {
        public string SaveLocation { get; }

        public MovieClientConfig(string url, string key, string saveLocation) : base(url, key)
        {
            SaveLocation = saveLocation;
        }
    }
}
