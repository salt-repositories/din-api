namespace Din.Domain.Clients.Radarr.Interfaces
{
    public interface IRadarrClientConfig
    {
        string Url { get; }
        string Key { get; }
        string SaveLocation { get; }
    }
}
