using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class DateEntry
{
    public DateEntry(string schemaVersion)
    {
        SchemaVersion = schemaVersion;
    }

    [JsonPropertyName("date")]
    public required string Date { get; set; }

    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; set; }
}
