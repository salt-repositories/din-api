using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.IpStack.Responses;

namespace Din.Domain.Clients.IpStack.Interfaces
{
    public interface IIpStackClient
    {
        Task<IpStackLocation> GetLocationAsync(string ip, CancellationToken cancellationToken);
    }
}
