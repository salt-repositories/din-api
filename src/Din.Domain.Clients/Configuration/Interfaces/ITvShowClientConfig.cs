namespace Din.Domain.Clients.Configuration.Interfaces
{
    public interface ITvShowClientConfig
    {
        string Url { get; }
        string Key { get; }
        string SaveLocation { get; }
    }
}
