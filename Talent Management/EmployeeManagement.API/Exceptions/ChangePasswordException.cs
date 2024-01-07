namespace EmployeeManagement.API.Exceptions
{
    public class ChangePasswordException : ApiException
    {
        public ChangePasswordException(string message) : base("CHANGE_PASSWORD_FAILED", System.Net.HttpStatusCode.BadRequest, message) { }
    }
}
