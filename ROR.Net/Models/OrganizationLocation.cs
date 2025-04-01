// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationLocation
{
    [JsonPropertyName("geonames_id")]
    public int GeonamesId { get; set; }

    [JsonPropertyName("geonames_details")]
    public required GeonamesDetails GeonamesDetails { get; set; }
}
