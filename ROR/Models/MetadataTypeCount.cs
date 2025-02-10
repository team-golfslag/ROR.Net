using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class MetadataTypeCount
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("id")]
    public OrganizationType Type { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }
}