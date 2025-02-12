using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Testing;

[TestFixture]
public class TestQuery
{
    private OrganizationService _service;

    [SetUp]
    public void Setup()
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();
        if (factory == null) throw new Exception("Failed to get logger factory");

        var logger = factory.CreateLogger<OrganizationService>();

        var httpClient = new HttpClient();
        _service = new OrganizationService(httpClient, logger);
    }

    [Test]
    public async Task TestSimpleQuery()
    {
        OrganizationsResult? result = await
            _service.Query()
                .WithQuery("Utrecht")
                .WithType(OrganizationType.Education)
                .WithNumberOfResults(100)
                .Execute();

        Assert.That(result, Is.Not.Null);

        MetadataTypeCount educationCount = result.Metadata.Types.First(m => m.Type == OrganizationType.Education);
        Assert.That(educationCount.Count, Is.GreaterThan(0));
    }

    [TearDown]
    public void TearDown() => _service.Dispose();
}