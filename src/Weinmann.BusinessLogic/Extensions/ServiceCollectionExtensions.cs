using Microsoft.Extensions.DependencyInjection;
using Weinmann.BusinessLogic.Services;
using Weinmann.Core.Services;

namespace Weinmann.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IBusinessLocationService, BusinessLocationService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
