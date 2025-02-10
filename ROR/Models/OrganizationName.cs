using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationName
{
    [JsonPropertyName("value")]
    public required string Value { get; set; }

    [JsonPropertyName("types")]
    public required List<OrganizationNameType> Types { get; set; }

    [JsonPropertyName("lang")]
    public string? Lang { get; set; }
}