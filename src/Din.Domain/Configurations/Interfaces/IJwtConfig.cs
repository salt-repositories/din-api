namespace Din.Domain.Configurations.Interfaces
{
    public interface IJwtConfig
    {
        string Issuer { get; set; }
        string Key { get;set; }
    }
}
