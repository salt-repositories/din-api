using System;
using System.Collections.Generic;

namespace Din.Application.WebAPI.Content.Responses
{
    public record ContentResponse
    {
        public Guid Id { get; init; }
        public int SystemId { get; init; }
        public string ImdbId { get; init; }
        public string Title { get; init; }
        public string Overview { get; init; }
        public IEnumerable<string> Genres { get; init; }
        public string Status { get; init; }
        public bool Downloaded { get; init; }
        public bool HasFile { get; init; }
        public string Year { get; init; }
        public DateTime Added { get; init; }
        public string PlexUrl { get; init; }
        public string PosterPath { get; init; }
        public ContentRatingResponse Ratings { get; init; }
    }
}