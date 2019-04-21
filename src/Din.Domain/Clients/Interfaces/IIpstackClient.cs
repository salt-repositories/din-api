using System.Threading.Tasks;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Clients.Interfaces
{
    public interface IIpStackClient
    {
        Task<IpStackLocation> GetLocation(string ip);
    }
}
