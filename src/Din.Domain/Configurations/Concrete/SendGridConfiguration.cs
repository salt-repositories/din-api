using Din.Domain.Configurations.Interfaces;

namespace Din.Domain.Configurations.Concrete
{
    public class SendGridConfiguration : ISendGridConfiguration
    {
        public string Key { get; set; }
        public string InviteTemplateId { get; set; }
    }
}
