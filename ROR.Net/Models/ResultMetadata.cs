// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public class ResultMetadata : ICombinable<ResultMetadata>
{
    [JsonPropertyName("continents")]
    public required List<MetadataContinentCount> Continents { get; set; }

    [JsonPropertyName("countries")]
    public required List<MetadataCountryCount> Countries { get; set; }

    [JsonPropertyName("statuses")]
    public required List<MetadataStatusCount> Statuses { get; set; }

    [JsonPropertyName("types")]
    public required List<MetadataTypeCount> Types { get; set; }

    public ResultMetadata Combine(ResultMetadata other) =>
        new()
        {
            Continents = MetadataCount.CollectCounts(Continents, other.Continents),
            Countries = MetadataCount.CollectCounts(Countries, other.Countries),
            Statuses = MetadataCount.CollectCounts(Statuses, other.Statuses),
            Types = MetadataCount.CollectCounts(Types, other.Types),
        };
}
