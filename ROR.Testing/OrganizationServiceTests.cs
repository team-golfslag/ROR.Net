using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Testing;

public class OrganizationServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly Mock<ILogger<OrganizationService>> _loggerMock;
    private readonly OrganizationService _organizationService;

    public OrganizationServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _loggerMock = new Mock<ILogger<OrganizationService>>();
        _organizationService = new OrganizationService(_httpClient, _loggerMock.Object);
    }

    [Fact]
    public async Task GetOrganization_ShouldReturnOrganization_WhenResponseIsValid()
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
            Types = [OrganizationType.Facility]
        };
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(organization)
            });

        // Act
        Organization? result = await _organizationService.GetOrganization("123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123", result.Id);
        Assert.Equal("Test Organization", result.Names[0].Value);
    }

    [Fact]
    public async Task GetOrganization_ShouldLogError_WhenRequestExceptionIsThrown()
    {
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Failed to get organization from ROR"));

        Organization? result = await _organizationService.GetOrganization("123");
        Assert.Null(result);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to get organization from ROR")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }

    [Fact]
    public async Task GetOrganization_ShouldLogError_WhenResponseIsInvalid()
    {
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Invalid JSON")
            });

        Organization? result = await _organizationService.GetOrganization("123");
        Assert.Null(result);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to deserialize organization from ROR")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}