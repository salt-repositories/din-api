using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommand : IAuthorizedRoleRequest, ITransactionRequest, IRequest<Account>
    {
        public AccountRole Role { get; }
        public Account Account { get; }
        public string Password { get; }

        public CreateAccountCommand(Account account, string password)
        {
            Role = AccountRole.Moderator;
            Account = account;
            Password = password;
        }
    }
}
