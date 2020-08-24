using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Infrastructure.DataAccess.Querying;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(DinContext context) : base(context)
        {
        }

        public async Task<IList<Account>> GetAccounts(QueryParameters queryParameters,
            CancellationToken cancellationToken)
        {
            IQueryable<Account> query = Context.Set<Account>()
                .Include(a => a.Image)
                .Include(a => a.Codes);

            query = query.ApplyQueryParameters(queryParameters);

            return await query.ToListAsync(cancellationToken);
        }

        public Task<Account> GetAccountById(Guid id, CancellationToken cancellationToken)
        {
            return Context.Account
                .Include(a => a.Image)
                .Include(a => a.Codes)
                .FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
        }

        public Task<Account> GetAccountByUsername(string username, CancellationToken cancellationToken)
        {
            return Context.Account
                .Include(a => a.Image)
                .Include(a => a.Codes)
                .FirstOrDefaultAsync(a => a.Username.Equals(username), cancellationToken);
        }

        public Task<Account> GetAccountByEmail(string email, CancellationToken cancellationToken)
        {
            return Context.Account
                .Include(a => a.Image)
                .Include(a => a.Codes)
                .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);
        }
    }
}