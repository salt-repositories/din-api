using System;
using System.Collections.Generic;
using Din.Data.Entities;
using Newtonsoft.Json;

namespace Din.ViewModels
{
    public class AccountViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("role")]
        public AccountRoll Role { get; set; }
        [JsonProperty("image")]
        public AccountImageEntity Image { get; set; }
        [JsonProperty("added_content")]
        public IEnumerable<AddedContentEntity> AddedContent { get; set; }
    }
}
