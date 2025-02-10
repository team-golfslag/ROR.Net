using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class GeonamesDetails
{
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("country_name")]
    public string? CountryName { get; set; }

    [JsonPropertyName("country_subdivision_code")]
    public string? CountrySubdivisionCode { get; set; }

    [JsonPropertyName("country_subdivision_name")]
    public string? CountrySubdivisionName { get; set; }

    [JsonPropertyName("lat")]
    public double? Latitude { get; set; }

    [JsonPropertyName("lng")]
    public double? Longitude { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}