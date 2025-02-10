using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationLink
{
    [JsonPropertyName("type")]
    public OrganizationLinkType Type { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}