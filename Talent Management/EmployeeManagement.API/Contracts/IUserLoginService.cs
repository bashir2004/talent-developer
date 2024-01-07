using EmployeeManagement.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Contracts
{
    public interface IUserLoginService
    {
        Task<IActionResult> LoginUser(UserLoginViewModel model);
        Task<IActionResult> GetUser();
        Task<IActionResult> ChangePassword(ChangePasswordViewModel model);
    }
}
