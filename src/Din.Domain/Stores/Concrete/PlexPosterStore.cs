using System.Collections.Generic;
using System.Linq;
using Din.Domain.Models.Dtos;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Stores.Concrete
{
    public class PlexPosterStore : IPlexPosterStore
    {
        private readonly HashSet<PlexPosterDto> _collection;

        public PlexPosterStore()
        {
            _collection = new HashSet<PlexPosterDto>();
        }

        public PlexPosterDto GetByTitle(string title)
        {
            return _collection.FirstOrDefault(c => c.Title.Equals(title));
        }

        public void AddOne(PlexPosterDto data)
        {
            _collection.Add(data);
        }
    }
}
