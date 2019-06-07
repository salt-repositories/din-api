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
    public class AddedContentRepository : BaseRepository, IAddedContentRepository
    {
        protected AddedContentRepository(DinContext context) : base(context)
        {
        }

        public async Task<IList<AddedContent>> GetAddedContentByAccountId(QueryParameters<AddedContent> queryParameters, Guid id,
            CancellationToken cancellationToken)
        {
            IQueryable<AddedContent> query = _context.Set<AddedContent>().Where(ac => ac.AccountId.Equals(id));
            query = query.ApplyQueryParameters(queryParameters);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
