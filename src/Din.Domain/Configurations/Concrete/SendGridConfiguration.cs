using Din.Domain.Configurations.Interfaces;

namespace Din.Domain.Configurations.Concrete
{
    public class SendGridConfiguration : ISendGridConfiguration
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string InviteTemplateId { get; set; }
        public string AuthorizationCodeTemplateId { get; set; }
    }
}
