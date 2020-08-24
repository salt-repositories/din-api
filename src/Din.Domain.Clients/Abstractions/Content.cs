using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public abstract class Content
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("imdbid")] public string ImdbId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("overview")] public string Overview { get; set; }
        [JsonProperty("genres")] public IEnumerable<string> Genres { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
        [JsonProperty("year")] public string Year { get; set; }
        [JsonProperty("added")] public DateTime Added { get; set; }
        [JsonProperty("ratings")] public ContentRating Ratings { get; set; }
    }
}
