using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IIpStackClient
    {
        Task<IpStackLocation> GetLocation(string ip);
    }
}
