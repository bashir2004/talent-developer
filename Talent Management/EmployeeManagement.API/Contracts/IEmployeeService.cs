using EmployeeManagement.API.ViewModels;

namespace EmployeeManagement.API.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> Get(Guid id);
        Task<dynamic> GetAll();
        Task<EmployeesListResultViewModel> GetList(EmployeeFilterViewModel model);
        Task<EmployeesListResultViewModel> GetList(int PageIndex, int PageSize);
        Task<EmployeeViewModel> CreateEmployee(CreateEmployeeViewModel model);
        Task<EmployeeViewModel> UpdateEmployee(UpdateEmployeeViewModel model);
        Task<bool> DeleteEmployee(Guid id);
    }
}
