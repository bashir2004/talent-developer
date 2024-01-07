using AutoMapper;
using EmployeeManagement.API.DBContexts;
using EmployeeManagement.API.Services;
using EmployeeManagement.API.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.API.Mappings;
using EmployeeManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.UnitTest.EmployeeServiceUnitTest
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<Employee, CreateEmployeeViewModel>();
            CreateMap<CreateEmployeeViewModel, Employee>();
            CreateMap<Employee, UpdateEmployeeViewModel>();
            CreateMap<UpdateEmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();
            // Add more mappings as needed
        }
    }
    public class EmployeeServiceUnitTest
    {
        private readonly IMapper _mapper;
        public EmployeeServiceUnitTest()
        {
            // Set up AutoMapper with your profile(s)
            var configuration = new MapperConfiguration(cfg =>
            {
                // Register your profile(s)
                cfg.AddProfile<EmployeeMapperProfile>();
                // Add more profiles if needed
            });

            // Create an IMapper instance
            _mapper = configuration.CreateMapper();
        }
        [Theory]
        [ClassData(typeof(EmployeeTestdata))]
        public void TestCreateEmployee(CreateEmployeeViewModel createEmployeeViewModel)
        {

            // Arrange
            var dbSetMock = new Mock<DbSet<Employee>>();

            Mock<AppDbContext> mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(d => d.Employees).Returns(dbSetMock.Object);


            Mock<ILogger<EmployeeService>> mockLogger = new Mock<ILogger<EmployeeService>>(); 
            Mock<EmployeeService> mockEmployeeService = new Mock<EmployeeService>(mockDbContext.Object, _mapper, mockLogger.Object);

            
            // Act
            Task<EmployeeViewModel> response = mockEmployeeService.Object.CreateEmployee(createEmployeeViewModel);

            // Assert
            Assert.NotNull(response);
        }
    }
}
