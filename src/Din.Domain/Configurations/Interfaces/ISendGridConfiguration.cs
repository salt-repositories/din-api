namespace Din.Domain.Configurations.Interfaces
{
    public interface ISendGridConfiguration
    {
        string Key { get; set; }
        string InviteTemplateId { get; set; }
    }
}
