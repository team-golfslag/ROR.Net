using ROR.Net.Models;

namespace ROR.Testing;

public class ResultMetadataTests
{
    private readonly ResultMetadata _metadata1 = new()
    {
        Continents = [new MetadataContinentCount { Count = 5, Id = "EU", Title = "Europe" }],
        Countries = [new MetadataCountryCount { Count = 10, Id = "NL", Title = "Netherlands" }],
        Statuses = [new MetadataStatusCount { Count = 20, Id = "active", Title = "Active" }],
        Types = [new MetadataTypeCount { Count = 12, Id = "university", Title = "University" }],
    };

    private readonly ResultMetadata _metadata2 = new()
    {
        Continents =
        [
            new MetadataContinentCount { Count = 3, Id = "EU", Title = "Europe" },
            new MetadataContinentCount { Count = 4, Id = "AS", Title = "Asia" }
        ],
        Countries =
        [
            new MetadataCountryCount { Count = 5, Id = "NL", Title = "Netherlands" },
            new MetadataCountryCount { Count = 8, Id = "DE", Title = "Germany" }
        ],
        Statuses =
        [
            new MetadataStatusCount { Count = 10, Id = "active", Title = "Active" },
            new MetadataStatusCount { Count = 5, Id = "inactive", Title = "Inactive" }
        ],
        Types =
        [
            new MetadataTypeCount { Count = 20, Id = "university", Title = "University" },
            new MetadataTypeCount { Count = 4, Id = "research_institute", Title = "Research Institute" }
        ],
    };

    [Fact]
    public void Combine_ShouldCombineContinents()
    {
        ResultMetadata combined = _metadata1.Combine(_metadata2);

        Assert.Equal(2, combined.Continents.Count);
        Assert.Contains(combined.Continents, c => c is { Id: "EU", Count: 8 });
        Assert.Contains(combined.Continents, c => c is { Id: "AS", Count: 4 });
    }

    [Fact]
    public void Combine_ShouldCombineCountries()
    {
        ResultMetadata combined = _metadata1.Combine(_metadata2);

        Assert.Equal(2, combined.Countries.Count);
        Assert.Contains(combined.Countries, c => c is { Id: "NL", Count: 15 });
        Assert.Contains(combined.Countries, c => c is { Id: "DE", Count: 8 });
    }

    [Fact]
    public void Combine_ShouldCombineStatuses()
    {
        ResultMetadata combined = _metadata1.Combine(_metadata2);

        Assert.Equal(2, combined.Statuses.Count);
        Assert.Contains(combined.Statuses, s => s is { Id: "active", Count: 30 });
        Assert.Contains(combined.Statuses, s => s is { Id: "inactive", Count: 5 });
    }

    [Fact]
    public void Combine_ShouldCombineTypes()
    {
        ResultMetadata combined = _metadata1.Combine(_metadata2);

        Assert.Equal(2, combined.Types.Count);
        Assert.Contains(combined.Types, t => t is { Title: "University", Count: 32 });
        Assert.Contains(combined.Types, t => t is { Title: "Research Institute", Count: 4 });
    }
}
