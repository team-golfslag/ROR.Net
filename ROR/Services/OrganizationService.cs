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
        OrganizationsResult? response =
            await httpClient.GetFromJsonAsync<OrganizationsResult>($"{BaseUrl}?{query}", _jsonSerializerOptions);
        if (response is not null) return response;

        logger.LogError("Failed to deserialize organizations from ROR");
        return null;
    }

    public async Task<Organization?> GetOrganization(string id)
    {
        Organization? response =
            await httpClient.GetFromJsonAsync<Organization>($"{BaseUrl}/{id}", _jsonSerializerOptions);
        if (response is not null) return response;

        logger.LogError("Failed to deserialize organization from ROR");
        return null;
    }

    public OrganizationQueryBuilder Query() => new(this);
}
