using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationLocation
{
    [JsonPropertyName("geonames_id")]
    public int GeonamesId { get; set; }

    [JsonPropertyName("geonames_details")]
    public required GeonamesDetails GeonamesDetails { get; set; }
}
