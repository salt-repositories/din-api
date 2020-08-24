using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class ContentPollStatusRepository : BaseRepository, IContentPollStatusRepository
    {
        public ContentPollStatusRepository(DinContext context) : base(context)
        {
        }
        public async Task<ContentPollStatus> GetPollStatusByContentIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var status = await Context.ContentPollStatus.FirstOrDefaultAsync(c => c.ContentId.Equals(id), cancellationToken);

            if (status != null)
            {
                return status;
            }

            status = new ContentPollStatus
            {
                ContentId = id
            };

            return (await Context.ContentPollStatus.AddAsync(status, cancellationToken)).Entity;
        }

        public ContentPollStatus GetPollStatusByContentId(Guid id)
        {
            var status = Context.ContentPollStatus.FirstOrDefault(c => c.ContentId.Equals(id));

            if (status != null)
            {
                return status;
            }

            status = new ContentPollStatus
            {
                ContentId = id
            };

            return Context.ContentPollStatus.Add(status).Entity;
        }
    }
}
