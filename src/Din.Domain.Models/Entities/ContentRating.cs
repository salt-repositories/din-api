using System;

namespace Din.Domain.Models.Entities
{
    public class ContentRating : IEntity
    {
        public Guid Id { get; set; }
        public Guid? MovieId { get; set; }
        public Guid? TvShowId { get; set; }

        public int Votes { get; set; }
        public double Value { get; set; }
    }
}
