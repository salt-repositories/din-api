using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public interface IContent : IEntity
    {
        int SystemId { get; set; }
        string ImdbId { get; set; }
        string Title { get; set; }
        string Overview { get; set; }
        string Status { get; set; }
        bool Downloaded { get; set; }
        bool HasFile { get; set; }
        string Year { get; set; }
        DateTime Added { get; set; }
        string PlexUrl { get; set; }
        string PosterPath { get; set; }
        ContentRating Ratings { get; set; }
        ICollection<string> AlternativeTitles { get; set; }
    }
}
