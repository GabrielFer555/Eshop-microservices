using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
			//add mediatR
			services.AddMediatR(opt =>
			{
				opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});
			return services;
		}
	}
}
