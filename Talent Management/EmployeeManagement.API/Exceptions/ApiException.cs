using System.Net;

namespace EmployeeManagement.API.Exceptions
{
    public class ApiException : AppException
    {
        public ApiException(string code) : base(code) { }

        public ApiException(string code, HttpStatusCode statusCode) : base(code, statusCode) { }

        public ApiException(string code, HttpStatusCode statusCode, string message) : base(code, statusCode, message) { }
    }
}
