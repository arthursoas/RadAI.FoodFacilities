using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadAI.FoodFacilities.Data.Repositories;

namespace RadAI.FoodFacilities.Data
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, string databaseName)
        {
            services.AddScoped<IPermitRepository, PermitRepository>();
            services.AddDbContext<FoodFacilitiesDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            return services;
        }
    }
}
