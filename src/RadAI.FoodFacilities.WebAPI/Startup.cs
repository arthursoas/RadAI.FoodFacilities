using Microsoft.AspNetCore.Authentication;
using RadAI.FoodFacilities.WebAPI.Configuration;
using RadAI.FoodFacilities.WebAPI.Settings;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RadAI.FoodFacilities.WebAPI
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var domainSettings = Configuration.GetSection(nameof(DomainSettings)).Get<DomainSettings>();

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddCors(options =>
            {
                options.AddPolicy("All", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSwaggerConfig();
            //services.AddDbContext<ShopBridgeDbContext>(options =>
            //    options.UseSqlServer(domainSettings.DatabaseSettings.ConnectionString));
            //services.AddDataRepositories();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfig();
            app.UseRouting();
            app.UseCors("All");
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
