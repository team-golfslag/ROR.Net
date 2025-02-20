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
