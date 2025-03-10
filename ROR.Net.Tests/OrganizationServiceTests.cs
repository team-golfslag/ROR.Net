using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Tests;

public class OrganizationServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<ILogger<OrganizationService>> _loggerMock;
    private readonly OrganizationService _organizationService;

    public OrganizationServiceTests()
    {
        _httpMessageHandlerMock = new();
        HttpClient httpClient = new(_httpMessageHandlerMock.Object);
        _loggerMock = new();
        _organizationService = new(httpClient, _loggerMock.Object);
    }

    [Fact]
    public async Task GetOrganization_ShouldReturnOrganization_WhenResponseIsValid()
    {
        Organization organization = new()
        {
            Id = "123",
            Admin = new()
            {
                Created = new()
                {
                    Date = "2021-01-01",
                    SchemaVersion = "v1.1",
                },
                DateEntry = new()
                {
                    Date = "2021-01-01",
                    SchemaVersion = "v1.1",
                },
            },
            Locations =
            [
                new OrganizationLocation
                {
                    GeonamesId = 1283416,
                    GeonamesDetails = new()
                    {
                        CountryCode = "NP",
                        CountryName = "Nepal",
                        Name = "Mount Everest",
                    },
                },
            ],
            Names =
            [
                new OrganizationName
                {
                    Value = "Test Organization",
                    Types = [OrganizationNameType.Label],
                    Lang = "en",
                },
            ],
            Types = [OrganizationType.Facility],
        };
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(organization),
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
                Content = new StringContent("Invalid JSON"),
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
