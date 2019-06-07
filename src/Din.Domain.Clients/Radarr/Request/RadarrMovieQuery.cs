namespace Din.Domain.Clients.Radarr.Request
{
    public class RadarrMovieQuery
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string SortKey { get; set; }
        public string SortDirection { get; set; }
    }
}
