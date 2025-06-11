// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ROR.Net.Exceptions;
using ROR.Net.Models;

namespace ROR.Net.Services;

/// <summary>
/// Service to interact with the ROR API.
/// </summary>
public class OrganizationService : IOrganizationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OrganizationService> _logger;
    private readonly OrganizationServiceOptions _options;

    public OrganizationService(
        HttpClient httpClient,
        IOptions<OrganizationServiceOptions> options,
        ILogger<OrganizationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options.Value;

        // configure the base address once
        _httpClient.BaseAddress = new(_options.BaseUrl);
    }

    /// <inheritdoc/>
    public async Task<OrganizationsResult?> PerformQueryAsync(string query)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<OrganizationsResult>(
                $"?{query}", _options.JsonSerializerOptions);
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Failed to get organizations from ROR (query={Query})", query);
            throw new RORServiceException(
                "Failed to get organizations from ROR", e);
        }
        catch (JsonException e)
        {
            _logger.LogError(e, "Failed to deserialize organizations from ROR (query={Query})", query);
            throw new RORServiceException(
                "Failed to deserialize organizations from ROR", e);
        }
    }

    /// <inheritdoc/>
    public async Task<Organization?> GetOrganizationAsync(string id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Organization>(
                $"{_options.BaseUrl}/{id}", _options.JsonSerializerOptions);
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Failed to get organization from ROR (id={Id})", id);
            throw new RORServiceException(
                "Failed to get organization from ROR", e);
        }
        catch (JsonException e)
        {
            _logger.LogError(e, "Failed to deserialize organization from ROR (id={Id})", id);
            throw new RORServiceException(
                "Failed to deserialize organization from ROR", e);
        }
    }

    /// <inheritdoc/>
    public OrganizationQueryBuilder Query() => new(this);
}