using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public class Genre : IEntity
    {
        public Guid Id { get; set; }
        public IEnumerable<TvShowGenre> TvShowGenres { get; set; }
        public string Name { get; set; }
    }
}
