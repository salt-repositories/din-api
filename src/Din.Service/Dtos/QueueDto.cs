using System;

namespace Din.Service.Dtos
{
    public class QueueDto
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
    }
}