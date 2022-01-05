using System;

namespace Din.Domain.Models.Entities
{
    public class ContentPollStatus : IEntity
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public DateTime PlexUrlPolled { get; set; }
        public DateTime PosterUrlPolled { get; set; }
    }
}
