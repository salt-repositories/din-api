using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.Service.Dto.Content
{
    public class SearchResultDto<TIdentifier, TSearchType> where TSearchType : SearchBase
    {
        public IEnumerable<TIdentifier> CurrentCollection { get; set; }
        public ICollection<TSearchType> QueryCollection { get; set; }
    }
}
