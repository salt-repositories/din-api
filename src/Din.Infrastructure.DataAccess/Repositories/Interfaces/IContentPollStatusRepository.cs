using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IContentPollStatusRepository : IBaseRepository
    {
        Task<ContentPollStatus> GetPollStatusByContentIdAsync(Guid id, CancellationToken cancellationToken);
        ContentPollStatus GetPollStatusByContentId(Guid id);
    }
}
