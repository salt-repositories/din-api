namespace Din.Domain.Clients.Configuration.Interfaces
{
    public interface ITokenConfig
    {
        string Issuer { get; }
        string Key { get; }
    }
}
