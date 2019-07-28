using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class SendAuthorizationCodeCommand : ITransactionRequest, IRequest
    {
        public string Email { get; }

        public SendAuthorizationCodeCommand(string email)
        {
            Email = email;
        }
    }
}
