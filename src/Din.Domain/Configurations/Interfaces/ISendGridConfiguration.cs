namespace Din.Domain.Configurations.Interfaces
{
    public interface ISendGridConfiguration
    {
        string Email { get; set; }
        string Name { get; set; }
        string Key { get; set; }
        string InviteTemplateId { get; set; }
        string AuthorizationCodeTemplateId { get; set; }
    }
}
