using System;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Requests
{
    public class MovieRequest
    {
        public Guid AccountId { get; set; }
        public SearchMovie Movie { get; set; }
    }
}
