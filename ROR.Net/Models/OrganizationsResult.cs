using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class OrganizationsResult : ICombinable<OrganizationsResult>
{
    [JsonPropertyName("items")]
    public required List<Organization> Organizations { get; set; }

    [JsonPropertyName("meta")]
    public required ResultMetadata Metadata { get; set; }

    [JsonPropertyName("number_of_results")]
    public int NumberOfResults { get; set; }

    [JsonPropertyName("time_taken")]
    public int TimeTaken { get; set; }

    public OrganizationsResult Combine(OrganizationsResult other) =>
        new()
        {
            Organizations = Organizations.Concat(other.Organizations).ToList(),
            Metadata = Metadata.Combine(other.Metadata),
            NumberOfResults = NumberOfResults + other.NumberOfResults,
            TimeTaken = TimeTaken + other.TimeTaken,
        };
}
