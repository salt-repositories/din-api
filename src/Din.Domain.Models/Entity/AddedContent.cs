using System;

namespace Din.Domain.Models.Entity
{

    public class AddedContent
    {
        public Guid Id { get; set; }
        public int SystemId { get; set; }
        public int ForeignId { get; set; }
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
        public Account Account { get; set; }
        public Guid AccountRef { get; set; }
    }

    public enum ContentType
    {
        Movie,
        TvShow
    }

    public enum ContentStatus
    {
        NotAvailable,
        Queued,
        Downloading,
        Stuck,
        Done
    }
}
