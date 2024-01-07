using AutoMapper;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.ViewModels;

namespace EmployeeManagement.API.Mappings
{
    public static class AutoMapperConfig
    {
        public static void RegisterMapperProfiles(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(mapperConfig =>
            {
                mapperConfig.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(services.BuildServiceProvider(), type));
                mapperConfig.CreateMap<AuthUserViewModel, AuthUser>();
                mapperConfig.CreateMap<AuthUser, AuthUserViewModel>();

                mapperConfig.CreateMap<EmployeeViewModel, Employee>();
                mapperConfig.CreateMap<Employee, EmployeeViewModel>();

                mapperConfig.CreateMap<UpdateEmployeeViewModel, Employee>();
                mapperConfig.CreateMap<Employee, UpdateEmployeeViewModel>();

                mapperConfig.CreateMap<CreateEmployeeViewModel, Employee>();
                mapperConfig.CreateMap<Employee, CreateEmployeeViewModel>();
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
