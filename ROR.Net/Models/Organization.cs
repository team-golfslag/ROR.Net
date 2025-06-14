// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

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
    /// The name of the organization, as it is known in ROR.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    /// <summary>
    /// The domains registered to a particular institution, not including the protocol, path portions,
    /// or query parameters that may exist in the URL for the organization's website.
    /// An organization's website is stored in the <see cref="Links" /> field.
    /// </summary>
    [JsonPropertyName("domains")]
    public List<string>? Domains { get; set; }

    /// <summary>
    /// The year the organization was established, written as four digits (YYYY).
    /// </summary>
    [JsonPropertyName("established")]
    public int? Established { get; set; }
    
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
    public List<string>? Links { get; set; }
    
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
