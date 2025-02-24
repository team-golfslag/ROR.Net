using System.Text.Json.Serialization;

namespace ROR.Net.Models;

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationExternalIdType>))]
public enum OrganizationExternalIdType
{
    Fundref,
    Grid,
    Isni,
    Wikidata,
}
