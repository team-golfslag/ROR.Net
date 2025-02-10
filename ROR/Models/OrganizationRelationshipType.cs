using System.Text.Json.Serialization;

namespace ROR.Net.Models;

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationRelationshipType>))]
public enum OrganizationRelationshipType
{
    Related,
    Parent,
    Child,
    Predecessor,
    Successor
}