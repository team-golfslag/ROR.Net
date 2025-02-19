using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class DateEntry
{
    [JsonPropertyName("date")]
    public required string Date { get; set; }

    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; set; }
}
