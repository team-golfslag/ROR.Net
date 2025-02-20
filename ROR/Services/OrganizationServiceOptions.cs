using System.Text.Json;
using System.Text.Json.Serialization;

namespace ROR.Net.Services;

public class OrganizationServiceOptions
{
    /// <summary>
    /// The base URL for the ROR API.
    /// </summary>
    /// <value>The default value is "https://api.ror.org/organizations".</value>
    public string BaseUrl { get; set; } = "https://api.ror.org/organizations";

    /// <summary>
    /// The options for the JSON serializer.
    /// </summary>
    /// <value>
    /// The default value is a new instance of <see cref="JsonSerializerOptions"/> with the following settings:
    /// <list type="bullet">
    /// <item><description><see cref="JsonSerializerOptions.PropertyNamingPolicy"/> = <see cref="JsonNamingPolicy.SnakeCaseLower"/></description></item>
    /// <item><description><see cref="JsonSerializerOptions.Converters"/> = { new <see cref="JsonStringEnumConverter"/> (<see cref="JsonNamingPolicy.SnakeCaseLower"/>) }</description></item>
    /// </list>
    /// </value>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) },
    };
}
