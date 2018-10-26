namespace Din.Service.Dto.Content
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
}
