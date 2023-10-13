using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weinmann.Core.Repositories;
using Weinmann.DataAccess.Repositories;

namespace Weinmann.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WeinmannDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("weinmann")));

        services.AddScoped<IBusinessLocationRepository, BusinessLocationRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
