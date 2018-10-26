using Din.Service.Config.Abstractions;
using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class UnsplashClientConfig : BaseClientConfig, IUnsplashClientConfig
    {
        public UnsplashClientConfig(string url, string key) : base(url, key)
        {
        }
    }
}
