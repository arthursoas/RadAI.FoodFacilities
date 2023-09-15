using RadAI.FoodFacilities.WebAPI.Managers;
using RadAI.FoodFacilities.WebAPI.Providers;

namespace RadAI.FoodFacilities.WebAPI
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddWebAPIDependencies(this IServiceCollection services)
        {
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IDataSFProvider, DataSFProvider>()
                .AddScoped<IPermitManager, PermitManager>();

            services.AddHttpClient("datasf");

            return services;
        }
    }
}
