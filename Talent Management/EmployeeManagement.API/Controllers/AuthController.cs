using EmployeeManagement.API.Contracts;
using EmployeeManagement.API.Exceptions;
using EmployeeManagement.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IUserLoginService _userLoginService;
        public AuthController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(TokenDto))]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            return await _userLoginService.LoginUser(login);
        }

        [Authorize]
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser()
        {
            return await _userLoginService.GetUser();
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userLoginService.ChangePassword(model);
                return Ok();

            }
            else
            {
                var errors = GetModelStateError();
                throw new ChangePasswordException(JsonSerializer.Serialize(errors));
            }
        }
    }
}
