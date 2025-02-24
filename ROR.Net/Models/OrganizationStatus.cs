using System.Text.Json.Serialization;

namespace ROR.Net.Models;

/// <summary>
/// Indication of whether the organization is active or not, based on a controlled list of status values.
/// Records can be active, inactive, or withdrawn.
/// </summary>
/// <remarks>
/// A record with a status of inactive or withdrawn may have one or more Successor organizations listed in its
/// relationships.
/// Successor relationships indicate that another organization continues the work of an organization that has become
/// inactive or has been withdrawn.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter<OrganizationStatus>))]
public enum OrganizationStatus
{
    /// <summary>
    /// An organization that is actively producing research outputs.
    /// </summary>
    Active,

    /// <summary>
    /// An organization that has ceased operation or producing research outputs.
    /// </summary>
    Inactive,

    /// <summary>
    /// A record that was created in error, such as a duplicate record or a record that is not in scope for the registry.
    /// </summary>
    Withdrawn,
}
