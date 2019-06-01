namespace Din.Domain.Clients.Configurations.Interfaces
{
    public interface IMovieClientConfig
    {
        string Url { get; }
        string Key { get; }
        string SaveLocation { get; }
    }
}
