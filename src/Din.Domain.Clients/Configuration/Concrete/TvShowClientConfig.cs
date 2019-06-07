using Din.Domain.Clients.Configuration.Interfaces;

namespace Din.Domain.Clients.Configuration.Concrete
{
    public class TvShowClientConfig : BaseClientConfig, ITvShowClientConfig
    {
        public string SaveLocation { get; set; }
    }
}
