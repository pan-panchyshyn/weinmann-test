using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Weinmann.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WeinmannDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("weinmann")));
    }
}
