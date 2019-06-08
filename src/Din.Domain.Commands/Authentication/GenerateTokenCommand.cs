using Din.Domain.Models.Dtos;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommand : IRequest<TokenDto>
    {
        public AuthenticationDto AuthenticationDetails { get; }

        public GenerateTokenCommand(AuthenticationDto authenticationDetails)
        {
            AuthenticationDetails = authenticationDetails;
        }
    }
}
