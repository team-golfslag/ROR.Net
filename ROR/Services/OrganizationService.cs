using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using ROR.Net.Models;

namespace ROR.Net.Services;

public sealed class OrganizationService(
    HttpClient httpClient,
    ILogger<OrganizationService> logger,
    OrganizationServiceOptions? options = null) : IDisposable
{
    private readonly OrganizationServiceOptions _options = options ?? new OrganizationServiceOptions();

    public void Dispose() => httpClient?.Dispose();

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

    public OrganizationQueryBuilder Query() => new(this);
}
