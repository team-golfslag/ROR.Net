using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationRelationShip
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("type")]
    public OrganizationRelationshipType Type { get; set; }
}
