// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

/// <summary>
/// The type of organization based on a controlled list of categories.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<OrganizationType>))]
public enum OrganizationType
{
    /// <summary>
    /// A university or similar institution involved in providing education and educating/employing researchers.
    /// </summary>
    Education,

    /// <summary>
    /// An organization that awards research funds.
    /// All records that are mapped to a Funder ID will be assigned this type, usually in conjunction with an additional
    /// organization type.
    /// </summary>
    Funder,

    /// <summary>
    /// A medical care facility such as hospital or medical clinic.
    /// Excludes medical schools, which should be categorized as “Education”.
    /// </summary>
    Healthcare,

    /// <summary>
    /// A private for-profit corporate entity involved in conducting or sponsoring research.
    /// </summary>
    Company,

    /// <summary>
    /// An organization involved in stewarding research and cultural heritage materials.
    /// Includes libraries, museums, and zoos.
    /// </summary>
    Archive,

    /// <summary>
    /// A non-profit and non-governmental organization involved in conducting or funding research.
    /// </summary>
    Nonprofit,

    /// <summary>
    /// An organization that is part of or operated by a national or regional government and that conducts or supports
    /// research.
    /// </summary>
    Government,

    /// <summary>
    /// A specialized facility where research takes place, such as a laboratory or telescope or dedicated research area.
    /// </summary>
    Facility,

    /// <summary>
    /// A category for any organization that does not fit the categories above.
    /// </summary>
    Other,
}
