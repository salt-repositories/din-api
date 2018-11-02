using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Din.Service.Clients.Interfaces
{
    public interface IContentClient
    {
        Task<IEnumerable<T>> GetCalendarAsync<T>(DateTime start, DateTime end);
        Task<IEnumerable<T>> GetQueue<T>();
    }
}
