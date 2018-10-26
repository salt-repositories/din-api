using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class GiphyClientConfig : BaseClientConfig, IGiphyClientConfig
    {
        public GiphyClientConfig(string url, string key) : base(url, key)
        {
        }
    }
}
