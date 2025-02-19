using Microsoft.Extensions.Logging;
using Moq;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Testing;

public class TestQuery : IAsyncLifetime
{
    private OrganizationService _service;

    public Task InitializeAsync()
    {
        HttpClient httpClient = new();
        ILogger<OrganizationService> logger = Mock.Of<ILogger<OrganizationService>>();

        _service = new OrganizationService(httpClient, logger);
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _service.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task TestSimpleQuery()
    {
        OrganizationsResult? result = await _service.Query()
            .WithQuery("Utrecht")
            .WithType(OrganizationType.Education)
            .WithNumberOfResults(100)
            .WithCountryCode("NL")
            .WithCountryCode("EU")
            .WithStatus(OrganizationStatus.Active)
            .CreatedDateUntil(DateTime.Now)
            .Execute();

        Assert.NotNull(result);

        MetadataTypeCount educationCount = result.Metadata.Types.First(m => m.Type == OrganizationType.Education);
        Assert.True(educationCount.Count > 0);
    }
}
