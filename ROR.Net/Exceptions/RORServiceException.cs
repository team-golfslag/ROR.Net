// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// 
// © Copyright Utrecht University (Department of Information and Computing Sciences)

namespace ROR.Net.Exceptions;

public class RORServiceException : Exception
{
    public RORServiceException(string exceptionMessage)
        : base(exceptionMessage)
    {
    }
    
    public RORServiceException(string exceptionMessage, Exception innerException)
        : base(exceptionMessage, innerException)
    {
    }
}
