using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Infrastructure.DataAccess.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DinContext _context;

        public AccountRepository(DinContext context)
        {
            _context = context;
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            IQueryable<Account> query = _context.Set<Account>();
 
            return await query.CountAsync(cancellationToken);
        }

        public async Task<IList<Account>> GetAccounts(QueryParameters<Account> queryParameters, CancellationToken cancellationToken)
        {
            IQueryable<Account> query = _context.Set<Account>().Include(a => a.Image);
            query = query.ApplyQueryParameters(queryParameters);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Account> GetAccount(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Account
                .Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id), cancellationToken);
        }
    }
}
