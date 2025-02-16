using Microsoft.Extensions.Configuration;
using Ordering.Application.Data;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
			// add Entity framework core
			services.AddScoped<ISaveChangesInterceptor, CustomSaveChangesInterceptor>();
			services.AddScoped<ISaveChangesInterceptor, DomainDispatcherInterceptor>(); 
			// defines as independency injection due to MediatR being in the constructor
			var connectionString = configuration.GetConnectionString("Database")!;
			services.AddDbContext<ApplicationDbContext>((sp, options) => { 
				options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
				options.UseSqlServer(connectionString);
			});
			services.AddScoped<IApplicationDbContext,ApplicationDbContext>();


			return services;
		}
		public static async Task <WebApplication> UseInfrastructureServices(this WebApplication app)
		{

			await app.UseAutoMigration();
			return app;
		}

	}
}
