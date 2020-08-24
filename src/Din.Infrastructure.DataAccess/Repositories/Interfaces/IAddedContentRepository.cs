using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IAddedContentRepository : IBaseRepository
    {
        Task<List<AddedContent>> GetAddedContentByAccountId(Guid id, QueryParameters queryParameters,
            AddedContentFilters filters,
            CancellationToken cancellationToken);
        Task<int> Count(Guid id, AddedContentFilters filters, CancellationToken cancellationToken);
    }
}
