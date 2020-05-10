using Din.Domain.Models.Entities;

namespace Din.Domain.Models.Querying
{
    public class AddedContentFilters
    {
        public ContentType? Type { get; set; }
        public ContentStatus? Status { get; set; }
    }
}
