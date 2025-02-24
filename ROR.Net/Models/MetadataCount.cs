using System.Text.Json.Serialization;

namespace ROR.Net.Models;

public abstract class MetadataCount
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }

    public static List<T> CollectCounts<T>(List<T> c1, List<T> c2) where T : MetadataCount
    {
        Dictionary<string, T>? counts = c1.ToDictionary(x => x.Id, x => x);

        foreach (T count in c2)
            if (counts.ContainsKey(count.Id))
                counts[count.Id].Count += count.Count;
            else
                counts.Add(count.Id, count);

        return counts.Values.ToList();
    }
}
