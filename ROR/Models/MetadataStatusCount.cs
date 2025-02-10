using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class MetadataStatusCount
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("id")]
    public OrganizationStatus Status { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }
}