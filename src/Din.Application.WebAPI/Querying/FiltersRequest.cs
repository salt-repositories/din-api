namespace Din.Application.WebAPI.Querying
{
    public class FiltersRequest
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public bool? Downloaded { get; set; }
        public string Year { get; set; }
    }
}