using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IAuthorizationCodeRepository : IBaseRepository
    {
        Task<IEnumerable<AccountAuthorizationCode>> GetAll(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
