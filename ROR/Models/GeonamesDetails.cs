using System.Text.Json.Serialization;

namespace ROR.Net.Models;

/// <summary>
/// Represents the details of a location from the Geonames database.
/// </summary>
public class GeonamesDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeonamesDetails"/> class.
    /// </summary>
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    [JsonPropertyName("country_name")]
    public string? CountryName { get; set; }

    /// <summary>
    /// Gets or sets the code of the country subdivision.
    /// </summary>
    [JsonPropertyName("country_subdivision_code")]
    public string? CountrySubdivisionCode { get; set; }

    /// <summary>
    /// Gets or sets the name of the country subdivision.
    /// </summary>
    [JsonPropertyName("country_subdivision_name")]
    public string? CountrySubdivisionName { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the location.
    /// </summary>
    [JsonPropertyName("lat")]
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the location.
    /// </summary>
    [JsonPropertyName("lng")]
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the name of the location.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
