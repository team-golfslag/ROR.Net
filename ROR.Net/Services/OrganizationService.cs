using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ROR.Net.Models;

namespace ROR.Net.Services;

/// <summary>
/// Service to interact with the ROR API
/// </summary>
public sealed class OrganizationService(
    HttpClient httpClient,
    ILogger<OrganizationService> logger,
    OrganizationServiceOptions? options = null) : IDisposable
{
    private readonly OrganizationServiceOptions _options = options ?? new OrganizationServiceOptions();

    public void Dispose() => httpClient?.Dispose();

    /// <summary>
    /// Perform a query to the ROR API
    /// </summary>
    /// <returns> The result of the query </returns>
    internal async Task<OrganizationsResult?> PerformQuery(string query)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<OrganizationsResult>(
                $"{_options.BaseUrl}?{query}", _options.JsonSerializerOptions);
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to get organizations from ROR");
            return null;
        }
        catch (JsonException e)
        {
            logger.LogError(e, "Failed to deserialize organizations from ROR");
            return null;
        }
    }

    /// <summary>
    /// Get an organization from the ROR API
    /// </summary>
    /// <param name="id"> The ID of the organization </param>
    /// <returns> The organization or <c>null</c> if it does not exist </returns>
    public async Task<Organization?> GetOrganization(string id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<Organization>($"{_options.BaseUrl}/{id}",
                _options.JsonSerializerOptions);
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to get organization from ROR");
            return null;
        }
        catch (JsonException e)
        {
            logger.LogError(e, "Failed to deserialize organization from ROR");
            return null;
        }
    }

    /// <summary>
    /// Query organizations from the ROR API
    /// </summary>
    /// <returns> A query builder </returns>
    public OrganizationQueryBuilder Query() => new(this);
}
