using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.Managers.Interfaces
{
    public interface IEmailManager
    {
        Task SendInvitation(string email, string username, string role, string code,
            CancellationToken cancellationToken);

        Task SendAuthorizationCode(string email, string username, string code, string userAgent, string ip,
            CancellationToken cancellationToken);
    }
}