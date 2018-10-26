namespace Din.Service.Config.Interfaces
{
    public interface ITokenConfig
    {
        string Issuer { get; }
        string Key { get; }
        string ClientId { get; }
        string Secret { get; }
    }
}
