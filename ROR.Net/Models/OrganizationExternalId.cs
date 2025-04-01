// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

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
