using Din.Domain.Config.Abstractions;
using Din.Domain.Config.Interfaces;

namespace Din.Domain.Config.Concrete
{
    public class UnsplashClientConfig : BaseClientConfig, IUnsplashClientConfig
    {
        public UnsplashClientConfig(string url, string key) : base(url, key)
        {
        }
    }
}
