using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationAdmin
{
    [JsonPropertyName("created")]
    public required DateEntry Created { get; set; }

    [JsonPropertyName("last_modified")]
    public required DateEntry DateEntry { get; set; }
}
