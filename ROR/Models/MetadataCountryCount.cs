using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class MetadataCountryCount
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("id")]
    public required string CountryCode { get; set; }

    [JsonPropertyName("title")]
    public required string CountryName { get; set; }
}