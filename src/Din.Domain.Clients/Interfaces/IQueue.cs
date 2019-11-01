using System;

namespace Din.Domain.Clients.Interfaces
{
    public interface IQueue<T>
    {
        T Content { get; set; }
        int Id { get; set; }
        string Title { get; set; }
        double Size { get; set; }
        double SizeLeft { get; set; }
        TimeSpan TimeLeft { get; set; }
        DateTime Eta { get; set; }
        string Status { get; set; }
        string DownloadId { get; set; }
        string Protocol { get; set; }
    }
}