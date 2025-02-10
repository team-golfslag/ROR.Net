using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class Organization
{
    /// <summary>
    /// The ROR ID for the organization, created and assigned by ROR.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// A field with information about the ROR record, including dates when the record was created and last modified.
    /// </summary>
    [JsonPropertyName("admin")]
    public required OrganizationAdmin Admin { get; set; }

    /// <summary>
    /// The domains registered to a particular institution, not including the protocol, path portions,
    /// or query parameters that may exist in the URL for the organization's website.
    /// An organization's website is stored in the <see cref="Links"/> field.
    /// </summary>
    [JsonPropertyName("domains")]
    public List<string>? Domains { get; set; }

    /// <summary>
    /// The year the organization was established, written as four digits (YYYY).
    /// </summary>
    [JsonPropertyName("established")]
    public int? Established { get; set; }

    /// <summary>
    /// Other identifiers for the organization (if available).
    /// ROR maps its IDs to four types of external identifiers:
    ///     GRID, Wikidata, ISNI, and the Crossref Open Funder Registry (formerly “FundRef”).
    /// </summary>
    [JsonPropertyName("external_ids")]
    public List<OrganizationExternalId>? ExternalIds { get; set; }

    /// <summary>
    /// The primary website and Wikipedia page for the organization.
    /// Only one website URL and one Wikipedia URL should be associated with the record.
    /// </summary>
    /// <remarks>
    /// In the case of websites with translated versions that use a language suffix like “/en”,
    /// the generic URL (without the language suffix) is used as long as the website resolves without it.
    /// Otherwise, the English version will be used.
    /// </remarks>
    [JsonPropertyName("links")]
    public List<OrganizationLink>? Links { get; set; }

    /// <summary>
    /// The location(s) of the organization, including continent name, continent code, country name,
    /// two-letter ISO-3166 country code, country subdivision name (e.g., Canadian province, Japanese prefecture or US state),
    /// country subdivision code, latitude, longitude, and specified location (usually city) name.
    /// Location data comes from GeoNames.
    /// </summary>
    [JsonPropertyName("locations")]
    public required List<OrganizationLocation> Locations { get; set; }

    /// <summary>
    /// Names for the organization, including four types of names: acronyms, aliases, labels, and the name.
    /// </summary>
    [JsonPropertyName("names")]
    public required List<OrganizationName> Names { get; set; }

    /// <summary>
    /// One or more organizations in ROR that the organization is related to.
    /// </summary>
    [JsonPropertyName("relationships")]
    public List<OrganizationRelationShip>? Relationships { get; set; }

    /// <summary>
    /// Indication of whether the organization is active or not, based on a controlled list of status values.
    /// </summary>
    [JsonPropertyName("status")]
    public OrganizationStatus OrganizationStatus { get; set; }

    /// <summary>
    /// The type of organization based on a controlled list of categories.
    /// </summary>
    [JsonPropertyName("types")]
    public required List<OrganizationType> Types { get; set; }
}