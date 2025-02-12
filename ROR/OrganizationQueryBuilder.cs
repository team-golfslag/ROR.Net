using System.Text;
using System.Web;
using ROR.Net.Models;
using ROR.Net.Services;

namespace ROR.Net;

public class OrganizationQueryBuilder
{
    private const int _pageSize = 20;
    private readonly OrganizationService _service;

    private readonly List<OrganizationStatus> _statusList = [];
    private readonly List<OrganizationType> _typeList = [];
    private readonly List<string> _organizationCountryCodeList = [];
    private readonly List<string> _organizationCountryNameList = [];
    private readonly List<string> _organizationContinentCodeList = [];
    private readonly List<string> _organizationContinentNameList = [];

    private DateTime? _createdDateFrom;
    private DateTime? _createdDateUntil;

    private DateTime? _modifiedDateFrom;
    private DateTime? _modifiedDateUntil;

    private int _numberOfResults = 20;
    private string? _query;

    internal OrganizationQueryBuilder(OrganizationService service) => _service = service;

    public OrganizationQueryBuilder WithStatus(OrganizationStatus status)
    {
        _statusList.Add(status);
        return this;
    }

    public OrganizationQueryBuilder WithType(OrganizationType type)
    {
        _typeList.Add(type);
        return this;
    }

    public OrganizationQueryBuilder WithCountryCode(string countryCode)
    {
        _organizationCountryCodeList.Add(countryCode);
        return this;
    }

    public OrganizationQueryBuilder WithCountryName(string countryName)
    {
        _organizationCountryNameList.Add(countryName);
        return this;
    }

    public OrganizationQueryBuilder WithContinentCode(string continentCode)
    {
        _organizationContinentCodeList.Add(continentCode);
        return this;
    }

    public OrganizationQueryBuilder WithContinentName(string continentName)
    {
        _organizationContinentNameList.Add(continentName);
        return this;
    }

    public OrganizationQueryBuilder CreatedDateFrom(DateTime createdDateFrom)
    {
        _createdDateFrom = createdDateFrom;
        return this;
    }

    public OrganizationQueryBuilder CreatedDateUntil(DateTime createdDateUntil)
    {
        _createdDateUntil = createdDateUntil;
        return this;
    }

    public OrganizationQueryBuilder ModifiedDateFrom(DateTime modifiedDateFrom)
    {
        _modifiedDateFrom = modifiedDateFrom;
        return this;
    }

    public OrganizationQueryBuilder ModifiedDateUntil(DateTime modifiedDateUntil)
    {
        _modifiedDateUntil = modifiedDateUntil;
        return this;
    }

    public OrganizationQueryBuilder WithQuery(string query)
    {
        _query = query;
        return this;
    }

    public OrganizationQueryBuilder WithNumberOfResults(int numberOfResults)
    {
        _numberOfResults = numberOfResults;
        return this;
    }

    public async Task<OrganizationsResult?> Execute()
    {
        var query = BuildQuery();
        var results = new List<OrganizationsResult>();
        foreach (string q in query)
        {
            OrganizationsResult? result = await _service.PerformQuery(q);
            if (result is not null) results.Add(result);
        }

        if (results.Count == 0) return null;

        OrganizationsResult first = results[0];
        if (results.Count == 1) return first;

        return results.Skip(1).Aggregate(first, (current, other) => current.Combine(other));
    }

    private List<string> BuildQuery()
    {
        if (_numberOfResults <= 0) throw new ArgumentException("Number of results must be greater than 0");
        if (_numberOfResults <= _pageSize) return [BuildQuery(null)];

        int pages = _numberOfResults / _pageSize;
        if (_numberOfResults % _pageSize > 0) pages++;

        return Enumerable.Range(1, pages).Select(page => BuildQuery(page)).ToList();
    }

    private string BuildQuery(int? page)
    {
        var components = new List<string>();
        if (page.HasValue) components.Add("page=" + page);

        var filter = new StringBuilder();
        filter.Append(string.Join(",", _statusList.Select(s => "status:" + s.ToString().ToLower())));
        filter.Append(string.Join(",", _typeList.Select(t => "types:" + t.ToString().ToLower())));
        filter.Append(string.Join(",", _organizationCountryCodeList.Select(cc => "country.country_code:" +  HttpUtility.UrlEncode(cc))));
        filter.Append(string.Join(",", _organizationCountryNameList.Select(cn => "locations.geonames_details.country_name:" +  HttpUtility.UrlEncode(cn))));
        filter.Append(string.Join(",", _organizationContinentCodeList.Select(cc => "locations.geonames_details.continent_code:" +  HttpUtility.UrlEncode(cc))));
        filter.Append(string.Join(",", _organizationContinentNameList.Select(cn => "locations.geonames_details.continent_name:" +  HttpUtility.UrlEncode(cn))));

        if (filter.Length > 0) components.Add("filter=" +  HttpUtility.UrlEncode(filter.ToString()));

        if (_query != null) components.Add("query=" + HttpUtility.UrlEncode(_query));

        var advancedQuery = new StringBuilder();
        if (_createdDateFrom.HasValue || _createdDateUntil.HasValue)
            components.Add("admin.created.date" +  HttpUtility.UrlEncode(GetFormattedDateRange(_createdDateFrom, _createdDateUntil)));

        if (_modifiedDateFrom.HasValue || _modifiedDateUntil.HasValue)
            components.Add("admin.last_modified.date" +  HttpUtility.UrlEncode(GetFormattedDateRange(_modifiedDateFrom, _modifiedDateUntil)));

        if (advancedQuery.Length > 0) components.Add("advanced_query=" +  HttpUtility.UrlEncode(advancedQuery.ToString()));
        
        return string.Join("&", components);
    }

    private static string GetFormattedDateRange(DateTime? from, DateTime? until)
    {
        string fromString = from.HasValue ? from.Value.ToString("yyyy-MM-dd") : "*";
        string untilString = until.HasValue ? until.Value.ToString("yyyy-MM-dd") : "*";
        return $"{{{fromString} TO% {untilString}}}";
    }
}
