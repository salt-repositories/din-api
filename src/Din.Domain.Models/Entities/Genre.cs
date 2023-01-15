using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public class Genre : IEntity
    {
        public Guid Id { get; set; }
        public ICollection<TvShowGenre> TvShowGenres { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public string Name { get; set; }
    }
}
