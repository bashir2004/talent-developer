using System.Net;

namespace EmployeeManagement.API.Exceptions
{
    public class OperationException : ApiException
    {
        public OperationException(string message) : base("OP_EXCEPTION", HttpStatusCode.BadRequest, message)
        {
        }
    }
}
