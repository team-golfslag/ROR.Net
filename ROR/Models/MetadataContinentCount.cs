using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class MetadataContinentCount
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("id")]
    public required string ContinentCode { get; set; }

    [JsonPropertyName("title")]
    public required string ContinentName { get; set; }
}