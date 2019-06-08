using Din.Domain.Models.Dtos;

namespace Din.Domain.Loggers.Interfaces
{
    public interface IAuthenticationRequest
    {
        AuthenticationDto AuthenticationDetails { get; }
    }
}
