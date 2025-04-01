// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

using System.Web;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Net;

/// <summary>
/// A builder class to create a query to search for organizations.
/// </summary>
public class OrganizationQueryBuilder
{
    /// <summary>
    /// The number of results per page.
    /// </summary>
    private const int PageSize = 20;

    /// <summary>
    /// The list of continent codes to filter by.
    /// </summary>
    private readonly List<string> _organizationContinentCodeList = [];

    /// <summary>
    /// The list of continent names to filter by.
    /// </summary>
    private readonly List<string> _organizationContinentNameList = [];

    /// <summary>
    /// The list of country codes to filter by.
    /// </summary>
    private readonly List<string> _organizationCountryCodeList = [];

    /// <summary>
    /// The list of country names to filter by.
    /// </summary>
    private readonly List<string> _organizationCountryNameList = [];

    /// <summary>
    /// The service to use to perform the query.
    /// </summary>
    private readonly OrganizationService _service;

    /// <summary>
    /// The list of organization statuses to filter by.
    /// </summary>
    private readonly List<OrganizationStatus> _statusList = [];

    /// <summary>
    /// The list of organization types to filter by.
    /// </summary>
    private readonly List<OrganizationType> _typeList = [];

    /// <summary>
    /// The start of range of created dates to filter by.
    /// </summary>
    private DateTime? _createdDateFrom;

    /// <summary>
    /// The end of range of created dates to filter by.
    /// </summary>
    private DateTime? _createdDateUntil;

    /// <summary>
    /// The start of range of modified dates to filter by.
    /// </summary>
    private DateTime? _modifiedDateFrom;

    /// <summary>
    /// The end of range of modified dates to filter by.
    /// </summary>
    private DateTime? _modifiedDateUntil;

    /// <summary>
    /// The amount of results to return.
    /// </summary>
    private int? _numberOfResults;

    /// <summary>
    /// The query to filter by.
    /// </summary>
    private string? _query;

    internal OrganizationQueryBuilder(OrganizationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Filter by organization status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple statuses. </remarks>
    public OrganizationQueryBuilder WithStatus(OrganizationStatus status)
    {
        _statusList.Add(status);
        return this;
    }

    /// <summary>
    /// Filter by organization type.
    /// </summary>
    /// <param name="type">The type to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple types. </remarks>
    public OrganizationQueryBuilder WithType(OrganizationType type)
    {
        _typeList.Add(type);
        return this;
    }

    /// <summary>
    /// Filter by country code.
    /// </summary>
    /// <param name="countryCode">The country code to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple country codes. </remarks>
    public OrganizationQueryBuilder WithCountryCode(string countryCode)
    {
        _organizationCountryCodeList.Add(countryCode);
        return this;
    }

    /// <summary>
    /// Filter by country name.
    /// </summary>
    /// <param name="countryName">The country name to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple country names. </remarks>
    public OrganizationQueryBuilder WithCountryName(string countryName)
    {
        _organizationCountryNameList.Add(countryName);
        return this;
    }

    /// <summary>
    /// Filter by continent code.
    /// </summary>
    /// <param name="continentCode">The continent code to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple continent codes. </remarks>
    public OrganizationQueryBuilder WithContinentCode(string continentCode)
    {
        _organizationContinentCodeList.Add(continentCode);
        return this;
    }

    /// <summary>
    /// Filter by continent name.
    /// </summary>
    /// <param name="continentName">The continent name to filter by.</param>
    /// <remarks> Can be called multiple times to filter by multiple continent names. </remarks>
    public OrganizationQueryBuilder WithContinentName(string continentName)
    {
        _organizationContinentNameList.Add(continentName);
        return this;
    }

    /// <summary>
    /// Filter by created date.
    /// </summary>
    /// <param name="createdDateFrom">The start of the range of created dates to filter by.</param>
    /// <remarks> When called without <see cref="CreatedDateUntil(DateTime)" />, it will filter until the end of time. </remarks>
    public OrganizationQueryBuilder CreatedDateFrom(DateTime createdDateFrom)
    {
        _createdDateFrom = createdDateFrom;
        return this;
    }

    /// <summary>
    /// Filter by created date.
    /// </summary>
    /// <param name="createdDateUntil">The end of the range of created dates to filter by.</param>
    /// <remarks> When called without <see cref="CreatedDateFrom(DateTime)" />, it will filter from the beginning of time. </remarks>
    public OrganizationQueryBuilder CreatedDateUntil(DateTime createdDateUntil)
    {
        _createdDateUntil = createdDateUntil;
        return this;
    }

    /// <summary>
    /// Filter by modified date.
    /// </summary>
    /// <param name="modifiedDateFrom">The start of the range of modified dates to filter by.</param>
    /// <remarks> When called without <see cref="ModifiedDateUntil(DateTime)" />, it will filter until the end of time. </remarks>
    public OrganizationQueryBuilder ModifiedDateFrom(DateTime modifiedDateFrom)
    {
        _modifiedDateFrom = modifiedDateFrom;
        return this;
    }

    /// <summary>
    /// Filter by modified date.
    /// </summary>
    /// <param name="modifiedDateUntil">The end of the range of modified dates to filter by.</param>
    /// <remarks> When called without <see cref="ModifiedDateFrom(DateTime)" />, it will filter from the beginning of time. </remarks>
    public OrganizationQueryBuilder ModifiedDateUntil(DateTime modifiedDateUntil)
    {
        _modifiedDateUntil = modifiedDateUntil;
        return this;
    }

    /// <summary>
    /// Filter by query.
    /// </summary>
    /// <param name="query">The query to filter by.</param>
    /// <remarks> Can only be called once. </remarks>
    /// <exception cref="InvalidOperationException"> Query can only be set once </exception>
    public OrganizationQueryBuilder WithQuery(string query)
    {
        if (_query != null)
            throw new InvalidOperationException("Query can only be set once");
        _query = query;
        return this;
    }

    /// <summary>
    /// Set the number of results to return.
    /// </summary>
    /// <param name="numberOfResults">The number of results to return.</param>
    /// <remarks> Must be greater than 0. Can only be set once. </remarks>
    /// <exception cref="InvalidOperationException"> Number of results can only be set once </exception>
    /// <exception cref="ArgumentException"> Number of results must be greater than 0 </exception>
    public OrganizationQueryBuilder WithNumberOfResults(int numberOfResults)
    {
        if (numberOfResults <= 0)
            throw new ArgumentException("Number of results must be greater than 0");
        if (_numberOfResults != null)
            throw new InvalidOperationException("Number of results can only be set once");
        _numberOfResults = numberOfResults;
        return this;
    }

    /// <summary>
    /// Execute the query.
    /// </summary>
    /// <returns> The result of the query. </returns>
    public async Task<OrganizationsResult?> Execute()
    {
        // Build the query
        List<string> query = BuildQueries();

        // Perform the query
        List<OrganizationsResult> results = [];
        foreach (string q in query)
        {
            OrganizationsResult? result = await _service.PerformQuery(q);
            if (result is not null) results.Add(result);
        }

        if (results.Count == 0) return null;

        // Combine the results
        OrganizationsResult first = results[0];
        if (results.Count == 1) return first;

        return results.Skip(1).Aggregate(first, (current, other) => current.Combine(other));
    }

    /// <summary>
    /// Build the query.
    /// </summary>
    /// <returns> The queries. </returns>
    /// <remarks> returns more than 1 query when the number of results is greater than the page size. </remarks>
    /// <exception cref="ArgumentException"> Number of results must be greater than 0 </exception>
    public List<string> BuildQueries()
    {
        int results = _numberOfResults ?? PageSize;
        if (results <= PageSize)
            return [BuildQuery(null)];

        int pages = results / PageSize;
        if (_numberOfResults % PageSize > 0) pages++;

        return Enumerable.Range(1, pages).Select(page => BuildQuery(page)).ToList();
    }

    /// <summary>
    /// Build the query for a specific page.
    /// </summary>
    /// <param name="page">The page to build the query for.</param>
    /// <returns> The query </returns>
    private string BuildQuery(int? page)
    {
        // Build the components of the query
        List<string> components = [];

        // Set the page
        if (page.HasValue)
            components.Add("page=" + page);

        // Build the filters
        List<string> filters =
        [
            string.Join(",", _statusList.Select(s => "status:" + s.ToString().ToLower())),
            string.Join(",", _typeList.Select(t => "types:" + t.ToString().ToLower())),
            string.Join(
                ",",
                _organizationCountryCodeList.Select(
                    cc => "country.country_code:" + HttpUtility.UrlEncode(cc))),
            string.Join(
                ",",
                _organizationCountryNameList.Select(
                    cn => "locations.geonames_details.country_name:" + HttpUtility.UrlEncode(cn))),
            string.Join(
                ",",
                _organizationContinentCodeList.Select(
                    cc => "locations.geonames_details.continent_code:" + HttpUtility.UrlEncode(cc))),
            string.Join(
                ",",
                _organizationContinentNameList.Select(
                    cn => "locations.geonames_details.continent_name:" + HttpUtility.UrlEncode(cn))),
        ];

        // Add the filters to the components
        if (filters.Count > 0)
            // Remove empty filters
            components.Add("filter=" + string.Join(",", filters.Where(f => f.Length > 0)));

        // Create the advanced query
        List<string> advancedQuery = [];
        if (_createdDateFrom.HasValue || _createdDateUntil.HasValue)
            advancedQuery.Add("admin.created.date:" +
                GetFormattedDateRange(_createdDateFrom, _createdDateUntil));

        if (_modifiedDateFrom.HasValue || _modifiedDateUntil.HasValue)
            advancedQuery.Add("admin.last_modified.date:" +
                GetFormattedDateRange(_modifiedDateFrom, _modifiedDateUntil));

        // If there is a query and advanced query, combine them
        if (_query != null && advancedQuery.Count > 0)
            advancedQuery.Add(HttpUtility.UrlEncode(_query));
        else
            // Otherwise, add only the query
            components.Add("query=" + HttpUtility.UrlEncode(_query));

        // If there is an advanced query, add it
        if (advancedQuery.Count > 0)
            components.Add("query.advanced=" + string.Join("%20AND%20", advancedQuery));

        // Return the query
        return string.Join("&", components);
    }

    /// <summary>
    /// Get a formatted date range.
    /// </summary>
    /// <param name="from">The start of the range.</param>
    /// <param name="until">The end of the range.</param>
    /// <returns> The formatted date range. </returns>
    /// <remarks> If <paramref name="from" /> is <c>null</c>, it will default to <see cref="DateTime.MinValue" />. </remarks>
    /// <remarks> If <paramref name="until" /> is <c>null</c>, it will default to <see cref="DateTime.MaxValue" />. </remarks>
    private static string GetFormattedDateRange(DateTime? from, DateTime? until)
    {
        from ??= DateTime.MinValue;
        until ??= DateTime.MaxValue;

        string fromString = from.Value.ToString("yyyy-MM-dd");
        string untilString = until.Value.ToString("yyyy-MM-dd");
        return $"%5B{fromString}%20TO%20{untilString}%5D";
    }
}
