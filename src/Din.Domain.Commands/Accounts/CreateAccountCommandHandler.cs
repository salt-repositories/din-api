using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
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
        private readonly ISendGridConfiguration _configuration;

        public CreateAccountCommandHandler(IAccountRepository repository, ISendGridConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.GetAccountByUsername(request.Account.Username, cancellationToken) != null)
            {
                throw new EntityCreationException("The username is already registered");
            }

            if (await _repository.GetAccountByEmail(request.Account.Email, cancellationToken) != null)
            {
                throw new EntityCreationException("The email is already registered");
            }

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

            await SendInvitationEmail(request.Account.Username, request.Account.Email, request.Account.Role,
                authenticationCode);

            _repository.Insert(request.Account);

            return request.Account;
        }

        private async Task SendInvitationEmail(string username, string email, AccountRole role, string code)
        {
            var client = new SendGridClient(_configuration.Key);

            var msg = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress("info@thedin.nl", "DIN"),
                new EmailAddress(email, username),
                _configuration.InviteTemplateId,
                new
                {
                    Username = username,
                    Role = role.ToString(),
                    Code = code
                }
            );

            await client.SendEmailAsync(msg);
        }
    }
}