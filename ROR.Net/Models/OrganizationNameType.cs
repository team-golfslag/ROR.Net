// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationNameType>))]
public enum OrganizationNameType
{
    /// <summary>
    /// One or more official acronyms or initialisms for the organization,
    /// typically consisting of the first letters of the words in the organization name
    /// (e.g., "UCLA" for “University of California, Los Angeles”).
    /// </summary>
    Acronym,

    /// <summary>
    /// Used for alternate forms of the organization name that may be now or previously in common use but are not
    /// official according to the organization's current website or policy
    /// (e.g., "London School of Economics" for "London School of Economics and Political Science").
    /// </summary>
    /// <remarks>
    /// This field may include both current and historical name variants.
    /// ROR does not currently identify which aliases are current versus historical,
    /// but future iterations of the ROR schema may differentiate between the two.
    /// </remarks>
    Alias,

    /// <summary>
    /// Displays equivalent forms of the organization name in one or more languages.
    /// </summary>
    Label,

    /// <summary>
    /// The name of the organization displayed most prominently on records in ROR's web search.
    /// </summary>
    RorDisplay,
}
