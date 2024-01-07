namespace EmployeeManagement.API.ViewModels
{
    public class EmployeesListResultViewModel
    {
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();
        public List<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
    }
}
