namespace Din.Domain.Config.Interfaces
{
    public interface ITokenConfig
    {
        string Issuer { get; }
        string Key { get; }
    }
}
