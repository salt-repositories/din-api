using System;

namespace Din.Domain.Models.Entities
{
    public class TvShowGenre
    { 
        public Guid TvshowId { get; set; }
        public TvShow TvShow { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
