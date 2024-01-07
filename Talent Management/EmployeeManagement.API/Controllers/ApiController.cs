using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    public class ApiController : ControllerBase
    {
        protected IEnumerable<string> GetModelStateError()
        {
            return ModelState.Keys.SelectMany(k => ModelState[k].Errors.Select(e => e.ErrorMessage));
        }
    }
}
