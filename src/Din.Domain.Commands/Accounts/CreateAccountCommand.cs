using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommand : IAuthorizedRoleRequest, IActivatedRequest, ITransactionRequest<Account>
    {
        public AccountRole AuthorizedRole { get; }
        public Account Account { get; }
        public string Password { get; }

        public CreateAccountCommand(Account account, string password)
        {
            AuthorizedRole = AccountRole.Moderator;
            Account = account;
            Password = password;
        }
    }
}
