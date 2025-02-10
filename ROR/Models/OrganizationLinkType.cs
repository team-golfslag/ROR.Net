using System.Text.Json.Serialization;

namespace ROR.Net.Models;

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationLinkType>))]
public enum OrganizationLinkType
{
    Website,
    Wikipedia
}