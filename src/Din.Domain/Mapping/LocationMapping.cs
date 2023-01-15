using Din.Domain.Clients.IpStack.Responses;
using Din.Domain.Models.Entities;

namespace Din.Domain.Mapping;

public static class LocationMapping
{
    public static LoginLocation ToEntity(this IpStackLocation location) => new()
    {
        ContinentCode = location.ContinentCode,
        ContinentName = location.ContinentName,
        CountryCode = location.CountryCode,
        CountryName = location.CountryName,
        RegionCode = location.RegionCode,
        RegionName = location.RegionName,
        City = location.City,
        ZipCode = location.ZipCode,
        Latitude = location.Latitude,
        Longitude = location.Longitude
    };
}