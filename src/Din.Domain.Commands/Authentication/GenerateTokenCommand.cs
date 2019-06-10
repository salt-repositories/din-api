using Din.Domain.Logging.Requests;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommand : IAuthenticationRequest, ITransactionRequest, IRequest<TokenDto>
    {
        public CredentialsDto Credentials { get; }

        public GenerateTokenCommand(CredentialsDto credentials)
        {
            Credentials = credentials;
        }
    }
}
