using Din.Domain.Configurations.Interfaces;

namespace Din.Domain.Configurations.Concrete
{
    public class TmdbClientConfig : ITmdbClientConfig
    {
        public string Key { get; set; }
    }
}
