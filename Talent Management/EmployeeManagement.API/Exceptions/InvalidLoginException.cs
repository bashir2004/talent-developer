using System.Net;

namespace EmployeeManagement.API.Exceptions
{
    public class InvalidLoginException : ApiException
    {
        public InvalidLoginException(string message) : base("LOGIN_FAILED", HttpStatusCode.Unauthorized, message) { }
    }
}
