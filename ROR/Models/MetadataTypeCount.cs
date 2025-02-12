namespace ROR.Net.Models;

public class MetadataTypeCount : MetadataCount
{
    public OrganizationType Type => Enum.Parse<OrganizationType>(Id, true);
}