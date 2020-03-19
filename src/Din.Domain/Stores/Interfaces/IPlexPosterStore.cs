using System.Collections.Generic;
using Din.Domain.Models.Dtos;

namespace Din.Domain.Stores.Interfaces
{
    public interface IPlexPosterStore
    {
        PlexPosterDto GetByTitle(string title);
        void AddOne(PlexPosterDto data);
    }
}
