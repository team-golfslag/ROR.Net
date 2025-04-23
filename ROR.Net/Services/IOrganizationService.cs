// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using ROR.Net.Models;

namespace ROR.Net.Services;

/// <summary>
/// Service to interact with the ROR API.
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// Perform a query against the ROR API.
    /// </summary>
    /// <param name="query">The raw query string (e.g. "query=Utrecht").</param>
    /// <returns>The full result object, or <c>null</c> on failure.</returns>
    Task<OrganizationsResult?> PerformQueryAsync(string query);

    /// <summary>
    /// GET /{id}
    /// Retrieve a single organization by ROR ID.
    /// </summary>
    /// <param name="id">The ROR identifier (e.g. "https://ror.org/04jsz6e67").</param>
    /// <returns>The matching <see cref="Organization"/>, or <c>null</c> if not found or on error.</returns>
    Task<Organization?> GetOrganizationAsync(string id);

    /// <summary>
    /// Start a fluent query build against ROR.
    /// </summary>
    OrganizationQueryBuilder Query();
}