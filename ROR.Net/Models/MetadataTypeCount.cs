// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

namespace ROR.Net.Models;

public class MetadataTypeCount : MetadataCount
{
    public OrganizationType Type => Enum.Parse<OrganizationType>(Id, true);
}
