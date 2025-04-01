// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationRelationshipType>))]
public enum OrganizationRelationshipType
{
    Related,
    Parent,
    Child,
    Predecessor,
    Successor,
}
