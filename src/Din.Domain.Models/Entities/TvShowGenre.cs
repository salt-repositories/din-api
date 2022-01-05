using System;

namespace Din.Domain.Models.Entities
{
    public class TvShowGenre : IEntity
    {
        public Guid Id { get; set; }
        public Guid TvshowId { get; set; }
        public TvShow TvShow { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
