using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ROR.Net;
using ROR.Net.Models;
using ROR.Net.Services;
using Xunit;

namespace ROR.Testing;

public class OrganizationQueryBuilderTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<ILogger<OrganizationService>> _loggerMock;
    private readonly OrganizationService _organizationService;

    public OrganizationQueryBuilderTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient httpClient = new(_httpMessageHandlerMock.Object);
        _loggerMock = new Mock<ILogger<OrganizationService>>();
        _organizationService = new OrganizationService(httpClient, _loggerMock.Object);
    }

    [Fact]
    public async Task Execute_ShouldReturnOrganizationsResult_WhenResponseIsValid()
    {
        Organization organization = new Organization
        {
            Id = "123",
            Admin = new OrganizationAdmin
            {
                Created = new DateEntry
                {
                    Date = "2021-01-01",
                    SchemaVersion = "v1.1",
                },
                DateEntry = new DateEntry
                {
                    Date = "2021-01-01",
                    SchemaVersion = "v1.1",
                },
            },
            Locations = [new OrganizationLocation
                {
                    GeonamesId = 1283416,
                    GeonamesDetails = new GeonamesDetails
                    {
                        CountryCode = "NP",
                        CountryName = "Nepal",
                        Name = "Mount Everest",
                    },
                },
            ],
            Names = [new OrganizationName
                {
                    Value = "Test Organization",
                    Types = [OrganizationNameType.Label],
                    Lang = "en",
                }
            ],
            Types = [OrganizationType.Facility],
        };

        ResultMetadata metadata = new()
        {
            Continents = [new MetadataContinentCount { Count = 5, Id = "EU", Title = "Europe" }],
            Countries = [new MetadataCountryCount { Count = 10, Id = "NL", Title = "Netherlands" }],
            Statuses = [new MetadataStatusCount { Count = 20, Id = "active", Title = "Active" }],
            Types = [new MetadataTypeCount { Count = 12, Id = "education", Title = "Education" }],
        };

        OrganizationsResult organizationResult = new()
        {
            Organizations = [organization],
            Metadata = metadata,
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(organizationResult),
            });

        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithQuery("Test")
            .WithNumberOfResults(1);

        OrganizationsResult? result = await queryBuilder.Execute();

        Assert.NotNull(result);
        Assert.Single(result.Organizations);
        Assert.Equal("123", result.Organizations[0].Id);
        Assert.Equal("Test Organization", result.Organizations[0].Names[0].Value);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenResponseIsInvalid()
    {
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Invalid JSON"),
            });

        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithQuery("Test")
            .WithNumberOfResults(1);

        OrganizationsResult? result = await queryBuilder.Execute();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Execute_ShouldLogError_WhenRequestExceptionIsThrown()
    {
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Failed to get organizations from ROR"));

        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithQuery("Test")
            .WithNumberOfResults(1);

        OrganizationsResult? result = await queryBuilder.Execute();

        Assert.Null(result);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to get organizations from ROR")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }

    [Fact]
    public void BuildQuery_ShouldThrowArgumentException_WhenNumberOfResultsIsZero()
    {
        OrganizationQueryBuilder queryBuilder = _organizationService.Query();

        Assert.Throws<ArgumentException>(() => queryBuilder.WithNumberOfResults(0));
    }


    [Fact]
    public void BuildQuery_ShouldThrowArgumentException_WhenNumberOfResultsIsSetMultipleTimes()
    {
        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithNumberOfResults(1);

        Assert.Throws<InvalidOperationException>(() => queryBuilder.WithNumberOfResults(2));
    }

    [Fact]
    public void BuildQuery_ShouldThrowArgumentException_WhenQueryIsSetMultipleTimes()
    {
        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithQuery("Test");

        Assert.Throws<InvalidOperationException>(() => queryBuilder.WithQuery("Test2"));
    }

    [Fact]
    public void BuildQuery_ShouldReturnCorrectQueryString()
    {
        OrganizationQueryBuilder queryBuilder = _organizationService
            .Query()
            .WithQuery("Test")
            .WithStatus(OrganizationStatus.Active)
            .WithType(OrganizationType.Education)
            .WithCountryCode("US")
            .WithCountryName("United States")
            .WithContinentCode("NA")
            .WithContinentName("North America")
            .CreatedDateFrom(new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .CreatedDateUntil(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ModifiedDateFrom(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ModifiedDateUntil(new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .WithNumberOfResults(20);

        string query = queryBuilder.BuildQuery()[0];

        Assert.Contains("status:active", query);
        Assert.Contains("types:education", query);
        Assert.Contains("country.country_code:US", query);
        Assert.Contains("locations.geonames_details.country_name:United+States", query);
        Assert.Contains("locations.geonames_details.continent_code:NA", query);
        Assert.Contains("locations.geonames_details.continent_name:North+America", query);
        Assert.Contains("admin.created.date:%5B2020-01-01%20TO%202021-01-01%5D", query);
        Assert.Contains("admin.last_modified.date:%5B2021-01-01%20TO%202022-01-01%5D", query);
    }
}