using System;
using System.Threading.Tasks;

namespace Din.Domain.Models.Entities
{

    public class AddedContent : IScopedEntity
    {
        public Guid Id { get; set; }
        public int SystemId { get; set; }
        public int ForeignId { get; set; }
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
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
