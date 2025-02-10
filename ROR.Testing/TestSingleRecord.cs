using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Testing;

[TestFixture]
public class SingleRecordTest
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
    [TestCase("028z9kw20", OrganizationNameType.Label, "Hogeschool Utrecht")]
    [TestCase("058kzes98", OrganizationNameType.Acronym, "UMU")]
    public async Task TestSingleRecordName(string id, OrganizationNameType type, string expectedName)
    {
        Organization? organization = await _service.GetOrganization(id);
        Assert.That(organization, Is.Not.Null);

        string name = organization.Names.First(n => n.Types.Contains(type)).Value;
        Assert.That(name, Is.EqualTo(expectedName));
    }

    [TearDown]
    public void TearDown() => _service.Dispose();
}