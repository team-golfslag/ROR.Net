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
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
    };

    private const string BaseUrl = "https://api.ror.org/v2/organizations";

    public async Task<OrganizationsResult?> PerformQuery(string query)
    {
        var response = await httpClient.GetFromJsonAsync<OrganizationsResult>($"{BaseUrl}?{query}", _jsonSerializerOptions);
        if (response is null)
        {
            logger.LogError("Failed to deserialize organizations from ROR");
            return null;
        }
        return response;
    }

    public async Task<Organization?> GetOrganization(string id)
    {
        var response = await httpClient.GetFromJsonAsync<Organization>($"{BaseUrl}/{id}", _jsonSerializerOptions);
        if (response is null)
        {
            logger.LogError("Failed to deserialize organization from ROR");
            return null;
        }

        return response;
    }

    public OrganizationQueryBuilder Query() => new(this);

    public void Dispose() => httpClient?.Dispose();
}