using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class ChangeAccountPasswordCommand : ITransactionRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthorizationCode { get; set; }

        public ChangeAccountPasswordCommand(string email, string password, string authorizationCode)
        {
            Email = email;
            Password = password;
            AuthorizationCode = authorizationCode;
        }
    }
}