using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Domain.Helpers.Interfaces
{
    public interface IPosterHelper
    {
        Task GetPoster(IContent content, CancellationToken cancellationToken);
        Task GetPoster(IEnumerable<IContent> content, CancellationToken cancellationToken);
    }
}
