using System;

namespace Din.Domain.Models.Entities
{
    public class ContentRating
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }

        public int Votes { get; set; }
        public double Value { get; set; }
    }
}
