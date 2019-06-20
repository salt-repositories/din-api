using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Context;
using Din.Domain.Extensions;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using SendGrid;
using SendGrid.Helpers.Mail;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountRepository _repository;
        private readonly IRequestContext _context;
        private readonly ISendGridConfiguration _configuration;

        public CreateAccountCommandHandler(IAccountRepository repository, IRequestContext context,
            ISendGridConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _configuration = configuration;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            request.Account.Hash = BC.HashPassword(request.Password);
            request.Account.Active = false;

            var authenticationCode = new string("").GenerateRandomString(30);

            request.Account.Codes = new List<AccountAuthenticationCode>
            {
                new AccountAuthenticationCode
                {
                    Account = request.Account,
                    Active = true,
                    Code = BC.HashPassword(authenticationCode),
                    Generated = DateTime.Now
                }
            };

            await SendInvitationEmail(request.Account.Username, request.Account.Email, authenticationCode);

            _repository.Insert(request.Account);

            return request.Account;
        }

        private async Task SendInvitationEmail(string username, string email, string code)
        {
            var url = $"{_context.GetApplicationUrl()}v1/accounts/activate?email={email}&code={code}";

            var client = new SendGridClient(_configuration.Key);

            var msg = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress("info@thedin.nl", "no-reply"),
                new EmailAddress(email, username),
                _configuration.InviteTemplateId,
                new
                {
                    Username = username,
                    ActivationLink = url
                }
            );

            await client.SendEmailAsync(msg);
        }
    }
}