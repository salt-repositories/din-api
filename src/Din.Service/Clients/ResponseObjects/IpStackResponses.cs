using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class IpStackLocation
    {
        [JsonProperty("continent_code")] public string ContinentCode { get; set; }
        [JsonProperty("continent_name")] public string ContinentName { get; set; }
        [JsonProperty("country_code")] public string CountryCode { get; set; }
        [JsonProperty("country_name")] public string CountryName { get; set; }
        [JsonProperty("region_code")] public string RegionCode { get; set; }
        [JsonProperty("region_name")] public string RegionName { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("zip")] public string ZipCode { get; set; }
        [JsonProperty("latitude")] public string Latitude { get; set; }
        [JsonProperty("longitude")] public string Longitude { get; set; }
    }
}