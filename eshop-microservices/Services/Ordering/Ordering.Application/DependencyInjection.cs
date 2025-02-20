using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddExceptionHandler<CustomExceptionHandler>();
			//add mediatR
			services.AddMediatR(opt =>
			{
				opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				opt.AddOpenBehavior(typeof(ValidationBehaviour<,>));
				opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
			});
			return services;
		}
	}
}
