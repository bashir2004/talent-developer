using EmployeeManagement.API.Contracts;
using EmployeeManagement.API.Exceptions;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _memoryCache;

        public EmployeeController(IEmployeeService employeeService, IMemoryCache memoryCache)
        {
            _employeeService = employeeService;
            _memoryCache = memoryCache;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue<List<Employee>>("GetAll", out var cachedData))
            {

                cachedData = await _employeeService.GetAll();
                _memoryCache.Set("GetAll", cachedData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }

            return new OkObjectResult(cachedData);
        }
        [HttpGet("get-list/{pageIndex}/{pageSize}")]

        public async Task<IActionResult> GetList(int pageIndex, int pageSize)
        {
            if (!_memoryCache.TryGetValue<EmployeesListResultViewModel>($"GetList_{pageIndex}_{pageSize}", out var cachedData))
            {

                cachedData = await _employeeService.GetList(pageIndex, pageSize);
                _memoryCache.Set($"GetList_{pageIndex}_{pageSize}", cachedData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
            return new OkObjectResult(cachedData);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _employeeService.Get(id);
            return new OkObjectResult(result);
        }
        [HttpPost("create")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(EmployeeViewModel))]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_memoryCache is MemoryCache concreteMemoryCache)
                {
                    concreteMemoryCache.Clear();
                }

                return new OkObjectResult(await _employeeService.CreateEmployee(model));
            }
            else
            {
                var errors = GetModelStateError();
                throw new CreateEmployeeException(JsonSerializer.Serialize(errors));
            }

        }
        [HttpPut("update")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(EmployeeViewModel))]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_memoryCache is MemoryCache concreteMemoryCache)
                {
                    concreteMemoryCache.Clear();
                }

                return new OkObjectResult(await _employeeService.UpdateEmployee(model));
            }
            else
            {
                var errors = GetModelStateError();
                throw new CreateEmployeeException(JsonSerializer.Serialize(errors));
            }
        }
        [HttpDelete("delete/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Boolean))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                if (_memoryCache is MemoryCache concreteMemoryCache)
                {
                    concreteMemoryCache.Clear();
                }

                return new OkObjectResult(await _employeeService.DeleteEmployee(id));
            }
            else
            {
                var errors = GetModelStateError();
                throw new CreateEmployeeException(JsonSerializer.Serialize(errors));
            }
        }
    }
}
