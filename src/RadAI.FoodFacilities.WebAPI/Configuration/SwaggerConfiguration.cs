using Microsoft.OpenApi.Models;

namespace RadAI.FoodFacilities.WebAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RadAI.FoodFacilities.WebAPI",
                    Description = "This web API is part of Rad AI backend assessment",
                    Version = "v1"
                });
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ThinkBridge.ShopBridge.WebAPI")
            );
        }
    }
}
