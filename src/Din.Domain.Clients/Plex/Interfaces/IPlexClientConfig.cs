using System;

namespace Din.Domain.Clients.Plex.Interfaces
{
    public interface IPlexClientConfig
    {
        string Url { get; }
        string Key { get; }
    }
}
