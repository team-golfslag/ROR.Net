// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

namespace ROR.Net.Models;

/// <summary>
/// Represents a count of metadata for a continent.
/// </summary>
public class MetadataContinentCount : MetadataCount
{
    /// <summary>
    /// Gets or sets the code of the continent.
    /// </summary>
    public string ContinentCode => Id;
}
