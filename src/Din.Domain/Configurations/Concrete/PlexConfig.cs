using System;
using Din.Domain.Configurations.Interfaces;

namespace Din.Domain.Configurations.Concrete
{
    public class PlexConfig : IPlexConfig
    {
        public string ServerGuid { get; set; }
    }
}
