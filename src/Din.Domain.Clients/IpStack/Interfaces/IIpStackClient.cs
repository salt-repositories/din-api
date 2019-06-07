using System.Threading.Tasks;
using Din.Domain.Clients.IpStack.Response;

namespace Din.Domain.Clients.IpStack.Interfaces
{
    public interface IIpStackClient
    {
        Task<IpStackLocation> GetLocationAsync(string ip);
    }
}
