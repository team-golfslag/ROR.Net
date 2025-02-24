using System.Text.Json.Serialization;

namespace ROR.Net.Models;

/// <summary>
/// Represents a date entry in the ROR database.
/// </summary>
public class DateEntry
{
    /// <summary>
    /// Gets or sets the date of the entry.
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    /// <summary>
    /// Gets or sets the schema version of the date entry.
    /// </summary>
    [JsonPropertyName("schema_version")]
    public required string SchemaVersion { get; set; }
}
