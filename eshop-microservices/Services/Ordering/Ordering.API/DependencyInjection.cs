
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using System.Text.Json.Serialization;

namespace Ordering.API
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) {
			services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database")!);

            services.AddExceptionHandler<CustomExceptionHandler>(); 
            //add services for carter
            services.AddControllers().AddJsonOptions(options =>
			options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddCarter();
			return services;
		}
		public static WebApplication UseApiServices(this WebApplication app) { //after app.build
		
			app.MapCarter();
            app.UseExceptionHandler(opt => { });
			app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
			{
				ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
			});

            return app;
		}
	}
}
