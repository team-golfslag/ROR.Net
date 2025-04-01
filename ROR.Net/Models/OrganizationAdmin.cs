// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationAdmin
{
    [JsonPropertyName("created")]
    public required DateEntry Created { get; set; }

    [JsonPropertyName("last_modified")]
    public required DateEntry DateEntry { get; set; }
}
