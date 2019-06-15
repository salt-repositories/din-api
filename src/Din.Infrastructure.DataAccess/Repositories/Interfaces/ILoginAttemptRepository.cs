using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface ILoginAttemptRepository : IBaseRepository
    {
        Task<LoginLocation> FindLoginLocationByCoordinates(string latitude, string longitude);
    }
}
