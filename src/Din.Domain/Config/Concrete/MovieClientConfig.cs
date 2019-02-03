using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
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
