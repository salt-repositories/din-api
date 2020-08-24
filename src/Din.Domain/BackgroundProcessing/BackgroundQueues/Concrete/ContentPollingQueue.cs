using System.Collections.Concurrent;
using Din.Domain.Models.Entities;

namespace Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete
{
    public class ContentPollingQueue : ConcurrentQueue<IContent>
    {
    }
}
