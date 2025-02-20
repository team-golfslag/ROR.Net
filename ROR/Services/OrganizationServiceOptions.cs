using System.Text.Json;
using System.Text.Json.Serialization;

namespace ROR.Net.Services;

public class OrganizationServiceOptions
{
    public string BaseUrl { get; set; } = "https://api.ror.org/organizations";

    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) },
    };
}
