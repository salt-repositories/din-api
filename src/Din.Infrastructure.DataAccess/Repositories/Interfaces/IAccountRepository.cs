using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository : IBaseRepository
    {
        Task<IList<Account>> GetAccounts(QueryParameters queryParameters, CancellationToken cancellationToken);
        Task<Account> GetAccountById(Guid id, CancellationToken cancellationToken);
        Task<Account> GetAccountByUsername(string username, CancellationToken cancellationToken);
        Task<Account> GetAccountByEmail(string email, CancellationToken cancellationToken);
    }
}
