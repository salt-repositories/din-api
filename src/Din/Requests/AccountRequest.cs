using Din.Data.Entities;
using Newtonsoft.Json;

namespace Din.Requests
{
    public class AccountRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("role")]
        public AccountRoll Role { get; set; }
    }
}
