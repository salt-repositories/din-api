using System;
using Din.Domain.Models.Entities;

namespace Din.Domain.Models.Querying
{
    public class AddedContentFilters
    {
        public string? Title { get; set; }
        public int? SystemId { get; set; }
        public int? ForeignId { get; set; }
        public ContentType? Type { get; set; }
        public ContentStatus? Status { get; set; }
    }
}
