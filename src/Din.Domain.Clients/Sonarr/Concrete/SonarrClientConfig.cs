using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Interfaces;

namespace Din.Domain.Clients.Sonarr.Concrete
{
    public class SonarrClientConfig : BaseClientConfig, ISonarrClientConfig
    {
        public string SaveLocation { get; set; }
    }
}
