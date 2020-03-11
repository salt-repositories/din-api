using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;

namespace Din.Domain.Helpers.Interfaces
{
    public interface IPosterHelper
    {
        Task GetPosters<T>(ICollection<T> content, CancellationToken cancellationToken) where T : Content;
    }
}
