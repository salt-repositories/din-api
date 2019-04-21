using System;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Requests
{
    public class TvShowRequest
    {
        public Guid AccountId { get; set; }
        public SearchTv TvShow { get; set; }
    }
}
