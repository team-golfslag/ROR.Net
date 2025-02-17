namespace ROR.Net.Models;

public class MetadataStatusCount : MetadataCount
{
    public OrganizationStatus Status => Enum.Parse<OrganizationStatus>(Id, true);
}
