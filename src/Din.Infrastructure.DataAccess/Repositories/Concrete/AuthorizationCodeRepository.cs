using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class AuthorizationCodeRepository : BaseRepository, IAuthorizationCodeRepository
    {
        public AuthorizationCodeRepository(DinContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccountAuthorizationCode>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.AuthorizationCode.ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
