using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> Count(CancellationToken cancellationToken);
        Task<IList<Account>> GetAccounts(QueryParameters<Account> queryParameters, CancellationToken cancellationToken);
        Task<Account> GetAccount(Guid id, CancellationToken cancellationToken);
    }
}
