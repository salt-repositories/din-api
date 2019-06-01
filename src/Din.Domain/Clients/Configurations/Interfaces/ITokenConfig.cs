namespace Din.Domain.Clients.Configurations.Interfaces
{
    public interface ITokenConfig
    {
        string Issuer { get; }
        string Key { get; }
    }
}
