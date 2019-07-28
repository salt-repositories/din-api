using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Managers.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using UAParser;

namespace Din.Domain.Managers.Concrete
{
    public class EmailManager : IEmailManager
    {
        private readonly ISendGridConfiguration _configuration;
        private readonly IIpStackClient _ipStackClient;
        private readonly ILogger _logger;

        public EmailManager(ISendGridConfiguration configuration, IIpStackClient ipStackClient, ILogger<EmailManager> logger)
        {
            _configuration = configuration;
            _ipStackClient = ipStackClient;
            _logger = logger;
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
            _logger.LogWarning($"address: {ip}");

            var deviceInfo = Parser.GetDefault().Parse(userAgent);
            var location = await _ipStackClient.GetLocationAsync(ip, cancellationToken);
           
            _logger.LogWarning(JsonConvert.SerializeObject(location));
            
            var client = new SendGridClient(_configuration.Key);

            var message = BuildMessage(_configuration.AuthorizationCodeTemplateId, email, username);
            message.SetTemplateData(new
            {
                Date = DateTime.Now.ToShortDateString(),
                Brand = !string.IsNullOrEmpty(deviceInfo.Device.Brand) 
                    ? $"{deviceInfo.Device.Brand}: {deviceInfo.Device.Family}"
                    : deviceInfo.Device.Family,
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