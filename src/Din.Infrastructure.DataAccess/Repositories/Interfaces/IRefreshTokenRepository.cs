using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IBaseRepository
    {
        Task<RefreshToken> Get(string token, CancellationToken cancellationToken);
        Task Invoke(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task Invoke(Guid accountId, CancellationToken cancellationToken);
    }
}
