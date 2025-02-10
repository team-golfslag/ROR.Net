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

    public ResultMetadata Combine(ResultMetadata other)
    {
        var continents = Continents.ToDictionary(x => x.ContinentCode, x => x);
        var countries = Countries.ToDictionary(x => x.CountryCode, x => x);
        var statuses = Statuses.ToDictionary(x => x.Status, x => x);
        var types = Types.ToDictionary(x => x.Type, x => x);

        foreach (MetadataContinentCount continent in other.Continents)
        {
            if (continents.ContainsKey(continent.ContinentCode))
                continents[continent.ContinentCode].Count += continent.Count;
            else
                continents.Add(continent.ContinentCode, continent);
        }

        foreach (MetadataCountryCount country in other.Countries)
        {
            if (countries.ContainsKey(country.CountryCode))
                countries[country.CountryCode].Count += country.Count;
            else
                countries.Add(country.CountryCode, country);
        }

        foreach (MetadataStatusCount status in other.Statuses)
        {
            if (statuses.ContainsKey(status.Status))
                statuses[status.Status].Count += status.Count;
            else
                statuses.Add(status.Status, status);
        }

        foreach (MetadataTypeCount type in other.Types)
        {
            if (types.ContainsKey(type.Type))
                types[type.Type].Count += type.Count;
            else
                types.Add(type.Type, type);
        }

        return new ResultMetadata
        {
            Continents = continents.Values.ToList(),
            Countries = countries.Values.ToList(),
            Statuses = statuses.Values.ToList(),
            Types = types.Values.ToList()
        };
    }
}