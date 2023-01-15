namespace Din.Domain.Clients.Abstractions
{
    public abstract class BaseClientConfig
    {
        public string Url { get; init; }
        public string Key { get; init; }
    }
}