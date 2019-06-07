using System;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Models.Request
{
    public class TvShowRequest
    {
        public Guid AccountId { get; set; }
        public SearchTv TvShow { get; set; }
    }
}
