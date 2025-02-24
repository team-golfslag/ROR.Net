using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationExternalId
{
    [JsonPropertyName("type")]
    public OrganizationExternalIdType Type { get; set; }

    [JsonPropertyName("all")]
    public required List<string> All { get; set; }

    [JsonPropertyName("preferred")]
    public string? Preferred { get; set; }
}
