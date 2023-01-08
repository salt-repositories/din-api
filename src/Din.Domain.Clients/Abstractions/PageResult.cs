using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions;

public class PageResult<T>
{
    [JsonProperty("page")] public int Page { get; set; }
    [JsonProperty("pageSize")] public int PageSize { get; set; }
    [JsonProperty("sortKey")] public string SortKey { get; set; }
    [JsonProperty("sortDirection")] public string SortDirection { get; set; }
    [JsonProperty("totalRecords")] public int TotalRecords { get; set; }
    [JsonProperty("records")] public IEnumerable<T> Records { get; set; }
}