using System;

namespace Din.Application.WebAPI.Content.Responses
{
    public class QueueResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Size { get; set; }
        public double SizeLeft { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public DateTime Eta { get; set; }
        public string Status { get; set; }
        public string DownloadId { get; set; }
        public string Protocol { get; set; }
        public ContentResponse Content { get; set; }
    }
}