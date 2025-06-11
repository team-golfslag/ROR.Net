// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using ROR.Net.Exceptions;
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

        HttpClient httpClient = new(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new("https://api.ror.org")
        };

        Mock<IOptions<OrganizationServiceOptions>> optionsMock = new();
        optionsMock
            .Setup(o => o.Value)
            .Returns(new OrganizationServiceOptions
            {
                BaseUrl = "https://api.ror.org",
                JsonSerializerOptions = new()
                {
                    PropertyNameCaseInsensitive = true
                }
            });

        _loggerMock = new();

        _organizationService = new(
            httpClient,
            optionsMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task GetOrganization_ShouldReturnOrganization_WhenResponseIsValid()
    {
        Organization organization = new()
        {
            Id = "123",
            Types =
            [
                OrganizationType.Facility
            ],
            Name = "Test Organization",
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
        Organization? result = await _organizationService.GetOrganizationAsync("123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123", result.Id);
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

        // Act & Assert
        await Assert.ThrowsAsync<RORServiceException>(() => _organizationService.GetOrganizationAsync("123"));
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

        // Act & Assert
        await Assert.ThrowsAsync<RORServiceException>(() => _organizationService.GetOrganizationAsync("123"));
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
