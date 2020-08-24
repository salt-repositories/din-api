using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Domain.Helpers.Interfaces
{
    public interface IPosterHelper
    {
        Task GetPosters<T>(IEnumerable<T> content, CancellationToken cancellationToken) where T : IContent;
    }
}
