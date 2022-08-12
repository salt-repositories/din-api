using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Domain.Helpers.Interfaces
{
    public interface IPlexHelper
    {
        Task CheckIsOnPlex(IContent content, CancellationToken cancellationToken);
        Task CheckIsOnPlex<T>(IEnumerable<T> content, CancellationToken cancellationToken) where T : IContent;
    }
}
