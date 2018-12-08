using System.Collections.Generic;

namespace Din.Service.Dtos
{
    public class MovieDto
    {
        public int SystemId { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public bool Downloaded { get; set; }
        public bool HasFile { get; set; }
    }

    public class MovieContainerDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortKey { get; set; }
        public string SortDirection { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<MovieDto> Records { get; set; }
    }
}
