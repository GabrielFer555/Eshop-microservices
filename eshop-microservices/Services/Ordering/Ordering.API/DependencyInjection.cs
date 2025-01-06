namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services) {

			//add services for carter

			return services;
		}
		public static WebApplication UseApiServices(this WebApplication app) { //after app.build
		
			//app.mapCarter();

			return app;
		}
	}
}
