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
        Task<IList<AddedContent>> GetAddedContentByAccountId(QueryParameters<AddedContent> queryParameters, Guid id, CancellationToken cancellationToken);
    }
}
