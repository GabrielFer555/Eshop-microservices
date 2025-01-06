using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
			// add Entity framework core
			var connectionString = configuration.GetConnectionString("Database")!;


			return services;
		}

	}
}
