using EmployeeManagement.API.Models;
using EmployeeManagement.API.ViewModels;

namespace EmployeeManagement.API.Contracts
{
    public interface ITokenService
    {
        Task<TokenDto> CreateAuthToken(AuthUser user);
    }
}
