using Din.Domain.Clients.Configurations.Abstractions;
using Din.Domain.Clients.Configurations.Interfaces;

namespace Din.Domain.Clients.Configurations.Concrete
{
    public class TvShowClientConfig : BaseClientConfig, ITvShowClientConfig
    {
        public string SaveLocation { get; set; }
    }
}
