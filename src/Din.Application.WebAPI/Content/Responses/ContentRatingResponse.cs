using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Content.Responses
{
    public record struct ContentRatingResponse
    {
        public int Votes { get; init; }
        public double Value { get; init; }

        public static implicit operator ContentRatingResponse(ContentRating contentRating) => new()
        {
            Votes = contentRating.Votes,
            Value = contentRating.Value
        };
        
        public static implicit operator ContentRatingResponse(Domain.Clients.Abstractions.ContentRating contentRating) => new()
        {
            Votes = contentRating.Votes,
            Value = contentRating.Value
        };
    }
}
