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
        public AddedContentRepository(DinContext context) : base(context)
        {
        }

        public async Task<IList<AddedContent>> GetAddedContentByAccountId(Guid id, QueryParameters<AddedContent> queryParameters, AddedContentFilters filters,
            CancellationToken cancellationToken)
        {
            IQueryable<AddedContent> query = Context.Set<AddedContent>().Where(ac => ac.AccountId.Equals(id));
            query = query.ApplyFilters(filters);
            query = query.ApplyQueryParameters(queryParameters);

            return await query.ToListAsync(cancellationToken);
        }

        public Task<int> Count(Guid id, AddedContentFilters filters, CancellationToken cancellationToken)
        {
            IQueryable<AddedContent> query = Context.Set<AddedContent>().Where(ac => ac.AccountId.Equals(id));
            query = query.ApplyFilters(filters);

            return query.CountAsync(cancellationToken);
        }
    }
}
