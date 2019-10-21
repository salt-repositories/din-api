using Din.Application.WebAPI.Content;

namespace Din.Application.WebAPI.Movies.Responses
{
    public class MovieQueueResponse : QueueResponse
    {
        public MovieResponse Movie { get; set; }
    }
}