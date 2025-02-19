using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using ROR.Net.Models;

namespace ROR.Net.Services;

public sealed class OrganizationService(
    HttpClient httpClient,
    ILogger<OrganizationService> logger) : IDisposable
{
    private const string BaseUrl = "https://api.ror.org/v2/organizations";

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) },
    };

    public void Dispose() => httpClient?.Dispose();

    internal async Task<OrganizationsResult?> PerformQuery(string query)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<OrganizationsResult>($"{BaseUrl}?{query}", _jsonSerializerOptions);
        } catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to get organizations from ROR");
            return null;
        } catch (JsonException e)
        {
            logger.LogError(e, "Failed to deserialize organizations from ROR");
            return null;
        }
    }

    public async Task<Organization?> GetOrganization(string id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<Organization>($"{BaseUrl}/{id}", _jsonSerializerOptions);
        } catch (HttpRequestException e)
        {
            logger.LogError(e, "Failed to get organization from ROR");
            return null;
        } catch (JsonException e)
        {
            logger.LogError(e, "Failed to deserialize organization from ROR");
            return null;
        }
    }

    public OrganizationQueryBuilder Query() => new(this);
}
