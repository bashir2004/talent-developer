namespace EmployeeManagement.API.Exceptions
{
    public class CreateEmployeeException : ApiException
    {
        public CreateEmployeeException(string message) : base("CREATE_EMPLOYEE_EXCEPTION", System.Net.HttpStatusCode.BadRequest, message) { }
    }
    public class UpdateEmployeeException : ApiException
    {
        public UpdateEmployeeException(string message) : base("UPDATE_EMPLOYEE_EXCEPTION", System.Net.HttpStatusCode.BadRequest, message) { }
    }
}
