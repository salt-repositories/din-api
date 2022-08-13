#nullable enable
using System.Collections.Concurrent;
using System.Collections.Generic;
using Din.Domain.Models.Entities;

namespace Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete
{
    public class ContentPollingQueue : ConcurrentQueue<IContent>
    {
        public IEnumerable<IContent> DequeueMultiple(int amount)
        {
            for (var i = 0; i < amount && Count > 0; i++)
            {
                if (TryDequeue(out var item))
                {
                    yield return item;
                }
            }
        }
    }
}
