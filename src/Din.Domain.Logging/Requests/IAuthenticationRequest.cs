using Din.Domain.Models.Dtos;
using MediatR;

namespace Din.Domain.Logging.Requests
{
    public interface IAuthenticationRequest : IBaseRequest
    {
        CredentialsDto Credentials { get; }
    }
}
