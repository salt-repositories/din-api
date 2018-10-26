using System;
using Din.Data.Entities;
using Newtonsoft.Json;

namespace Din.Service.Dto.Context
{
    public class LoginAttemptDto
    {
        public string Username { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string PublicIp { get; set; }
        public LoginLocationDto Location { get; set; }
        public DateTime DateAndTime { get; set; }
        public LoginStatus Status { get; set; }
    }

    public class LoginLocationDto
    {
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
