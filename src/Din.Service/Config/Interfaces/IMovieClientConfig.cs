namespace Din.Service.Config.Interfaces
{
    public interface IMovieClientConfig
    {
        string Url { get; }
        string Key { get; }
        string SaveLocation { get; }
    }
}
