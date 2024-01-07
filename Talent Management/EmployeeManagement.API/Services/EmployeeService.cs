using AutoMapper;
using EmployeeManagement.API.Contracts;
using EmployeeManagement.API.DBContexts;
using EmployeeManagement.API.Exceptions;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;
        public EmployeeService(AppDbContext dbContext,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<EmployeeViewModel> CreateEmployee(CreateEmployeeViewModel model)
        {
            try
            {
                var employee = _mapper.Map<Employee>(model);

                if (employee != null)
                {
                    _dbContext.Employees.Add(employee);
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<EmployeeViewModel>(employee);
                }

                throw new CreateEmployeeException("Invalid employee input.");
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_CreateEmployee(Username: {model.Username}): error saving employee, {ex.Message}.");
                throw new CreateEmployeeException("Error occurred in saving the employee data.");
            }
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            try
            {
                var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee != null)
                {
                    _dbContext.Employees.Remove(employee);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_DeleteEmployee(error deleting employee, {ex.Message}.");
            }
            return false;
        }

        public async Task<EmployeeViewModel> Get(Guid id)
        {
            try
            {
                var employee = await _dbContext
                    .Employees
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (employee == null)
                {
                    throw new OperationException("No Employee data found.");
                }

                return _mapper.Map<EmployeeViewModel>(employee);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_Get(id: {id}, error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the requested employee data.");
            }
        }

        public async Task<dynamic> GetAll()
        {
            try
            {
                return await _dbContext.Employees
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_GetAll(error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the employees lists data.");
            }
        }
        public async Task<EmployeesListResultViewModel> GetList(int PageIndex, int PageSize)
        {
            try
            {
                var result = _dbContext.Employees
                    .AsNoTracking();

                var totalRecords = result.Count();

                var records = (await result
                    .Skip((PageIndex -1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync())
                    .Select(c => _mapper.Map<EmployeeViewModel>(c))
                    .ToList();

                var endIndex = totalRecords * 1.0m / PageSize;
                return new EmployeesListResultViewModel
                {
                    Pagination = new PaginationViewModel
                    {
                        TotalRecords = totalRecords,
                        CurrentPageRecords = records.Count(),
                        PageSize = PageSize,
                        PageIndex = PageIndex,
                        StartIndex = 0,
                        EndIndex = (int)Math.Ceiling(endIndex)
                    },
                    Employees = records
                };

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_GetList(error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the employees list.");
            }
        }
        public async Task<EmployeesListResultViewModel> GetList(EmployeeFilterViewModel model)
        {
            try
            {
                var result = _dbContext.Employees
                    .AsNoTracking();


                var totalRecords = result.Count();

                var records = (await result
                    .Skip(model.PageIndex * model.PageSize)
                    .Take(model.PageSize)
                    .ToListAsync())
                    .Select(c => _mapper.Map<EmployeeViewModel>(c))
                    .ToList();

                var endIndex = totalRecords * 1.0m / model.PageSize;
                return new EmployeesListResultViewModel
                {
                    Pagination = new PaginationViewModel
                    {
                        TotalRecords = totalRecords,
                        CurrentPageRecords = records.Count(),
                        PageSize = model.PageSize,
                        PageIndex = model.PageIndex,
                        StartIndex = 0,
                        EndIndex = (int)Math.Ceiling(endIndex)
                    },
                    Employees = records
                };

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_GetList(error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the employees list.");
            }
        }

        public async Task<EmployeeViewModel> UpdateEmployee(UpdateEmployeeViewModel model)
        {
            try
            {
                var employee = _mapper.Map<Employee>(model);

                if (employee != null)
                {
                    if (model.Id.HasValue)
                    {
                        var employeeToUpdate = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);

                        if (employeeToUpdate == null)
                        {
                            throw new UpdateEmployeeException("Employee not found");
                        }

                        employeeToUpdate.Username = model.Username;
                        employeeToUpdate.Email = model.Email;
                        employeeToUpdate.PhoneNumber = model.PhoneNumber;
                        employeeToUpdate.SkillSet = model.SkillSet;
                        employeeToUpdate.Hobbies = model.Hobbies;

                        _dbContext.Employees.Update(employeeToUpdate);
                    }
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<EmployeeViewModel>(employee);
                }

                throw new UpdateEmployeeException("Invalid employee input.");
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - EmployeeService_UpdateEmployee(Username: {model.Username}): error saving employee, {ex.Message}.");
                throw new UpdateEmployeeException("Error occurred in saving the employee data.");
            }
        }
    }
}
