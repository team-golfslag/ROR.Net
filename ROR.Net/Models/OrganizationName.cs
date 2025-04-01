// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

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
