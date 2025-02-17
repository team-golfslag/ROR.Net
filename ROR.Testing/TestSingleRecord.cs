using Microsoft.Extensions.Logging;
using Moq;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Testing;

public class SingleRecordTest : IAsyncLifetime
{
    private OrganizationService _service;

    public Task InitializeAsync()
    {
        ILogger<OrganizationService> logger = Mock.Of<ILogger<OrganizationService>>();
        HttpClient httpClient = new();
        _service = new OrganizationService(httpClient, logger);

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _service.Dispose();
        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("028z9kw20", OrganizationNameType.Label, "Hogeschool Utrecht")]
    [InlineData("058kzes98", OrganizationNameType.Acronym, "UMU")]
    public async Task TestSingleRecordName(string id, OrganizationNameType type, string expectedName)
    {
        Organization? organization = await _service.GetOrganization(id);
        Assert.NotNull(organization);

        string name = organization.Names.First(n => n.Types.Contains(type)).Value;
        Assert.Equal(expectedName, name);
    }
}
