using Din.Domain.Loggers;
using Din.Domain.Loggers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommand : IAuthenticationRequest, ITransactionRequest, IRequest<TokenDto>
    {
        public AuthenticationDto AuthenticationDetails { get; }

        public GenerateTokenCommand(AuthenticationDto authenticationDetails)
        {
            AuthenticationDetails = authenticationDetails;
        }
    }
}
