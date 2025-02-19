using System.Text.Json.Serialization;

namespace ROR.Net.Models;

/// <summary>
/// Represents a date entry in the ROR database.
/// </summary>
public class DateEntry
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateEntry"/> class.
    /// </summary>
    /// <param name="schemaVersion">The schema version of the date entry.</param>
    public DateEntry(string schemaVersion)
    {
        SchemaVersion = schemaVersion;
    }
    
    /// <summary>
    /// Gets or sets the date of the entry.
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    /// <summary>
    /// Gets or sets the schema version of the date entry.
    /// </summary>
    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; set; }
}
