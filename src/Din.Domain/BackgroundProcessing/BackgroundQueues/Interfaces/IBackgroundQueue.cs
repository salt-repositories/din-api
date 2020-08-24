
namespace Din.Domain.BackgroundProcessing.BackgroundQueues.Interfaces
{
    public interface IBackgroundQueue<T>
    {
        void Enqueue(T item);
    }
}
