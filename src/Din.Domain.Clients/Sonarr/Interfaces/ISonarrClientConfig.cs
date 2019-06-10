namespace Din.Domain.Clients.Sonarr.Interfaces
{
    public interface ISonarrClientConfig
    {
        string Url { get; }
        string Key { get; }
        string SaveLocation { get; }
    }
}
