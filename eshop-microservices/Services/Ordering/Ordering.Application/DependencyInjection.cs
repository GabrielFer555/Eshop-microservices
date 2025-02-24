using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			var executingAssembly = Assembly.GetExecutingAssembly();	
			services.AddExceptionHandler<CustomExceptionHandler>();
			services.AddMessageBroker(configuration, executingAssembly);
			//add mediatR
			services.AddMediatR(opt =>
			{
				opt.RegisterServicesFromAssembly(executingAssembly);
				opt.AddOpenBehavior(typeof(ValidationBehaviour<,>));
				opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
			});
			services.AddFeatureManagement();
			return services;
		}
	}
}
