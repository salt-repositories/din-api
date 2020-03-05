using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        Task SendInvitation(string email, string username, string role, string code,
            CancellationToken cancellationToken);

        Task SendAuthorizationCode(string email, string username, string code, string userAgent, string ip,
            CancellationToken cancellationToken);
    }
}