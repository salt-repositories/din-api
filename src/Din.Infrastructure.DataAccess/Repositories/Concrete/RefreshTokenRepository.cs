using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DinContext context) : base(context)
        {
        }

        public Task<RefreshToken> Get(string token, CancellationToken cancellationToken)
        {
            return Context.RefreshToken.FirstOrDefaultAsync(r => r.Token.Equals(token), cancellationToken);
        }

        public Task Invoke(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            Context.RefreshToken.Remove(refreshToken);

            return Task.CompletedTask;
        }

        public async Task Invoke(Guid accountId, CancellationToken cancellationToken)
        {
            var token = await Context.RefreshToken.FirstOrDefaultAsync(r => r.AccountIdentity.Equals(accountId), cancellationToken);

            if (token == null)
            {
                return;
            }

            Context.RefreshToken.Remove(token);
        }
    }
}
