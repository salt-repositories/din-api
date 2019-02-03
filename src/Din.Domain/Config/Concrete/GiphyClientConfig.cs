using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
{
    public class GiphyClientConfig : BaseClientConfig, IGiphyClientConfig
    {
        public GiphyClientConfig(string url, string key) : base(url, key)
        {
        }
    }
}
