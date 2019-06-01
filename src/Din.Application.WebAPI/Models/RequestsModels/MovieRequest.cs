using System;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Models.RequestsModels
{
    public class MovieRequest
    {
        public Guid AccountId { get; set; }
        public SearchMovie Movie { get; set; }
    }
}
