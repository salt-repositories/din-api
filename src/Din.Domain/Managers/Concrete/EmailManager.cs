using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Managers.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using UAParser;

namespace Din.Domain.Managers.Concrete
{
    public class EmailManager : IEmailManager
    {
        private readonly ISendGridConfiguration _configuration;
        private readonly IIpStackClient _ipStackClient;

        public EmailManager(ISendGridConfiguration configuration, IIpStackClient ipStackClient)
        {
            _configuration = configuration;
            _ipStackClient = ipStackClient;
        }

        public async Task SendInvitation(string email, string username, string role, string code,
            CancellationToken cancellationToken)
        {
            var client = new SendGridClient(_configuration.Key);

            var message = BuildMessage(_configuration.InviteTemplateId, email, username);
            message.SetTemplateData(new
            {
                Username = username,
                Role = role,
                Code = code
            });

            await client.SendEmailAsync(message, cancellationToken);
        }

        public async Task SendAuthorizationCode(string email, string username, string code, string userAgent, string ip,
            CancellationToken cancellationToken)
        {
            var deviceInfo = Parser.GetDefault().Parse(userAgent);
            var location = await _ipStackClient.GetLocationAsync(ip, cancellationToken);

            var client = new SendGridClient(_configuration.Key);

            var message = BuildMessage(_configuration.AuthorizationCodeTemplateId, email, username);
            message.SetTemplateData(new
            {
                Date = DateTime.Now.ToShortDateString(),
                Brand = deviceInfo.Device.Family,
                OS = deviceInfo.OS.Family,
                Browser = deviceInfo.UA.Family,
                location.City,
                Country = location.CountryName,
                Code = code
            });

            await client.SendEmailAsync(message, cancellationToken);
        }

        private SendGridMessage BuildMessage(string templateId, string email, string username)
        {
            return MailHelper.CreateSingleTemplateEmail(
                new EmailAddress(_configuration.Email, _configuration.Name),
                new EmailAddress(email, username),
                templateId,
                new { }
            );
        }
    }
}