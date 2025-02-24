using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker
            (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null) {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                if(assembly is not null) //In case an assembly is passed on the parameters, it tell the message broker that application is a consumer/not a publisher
                {
                    config.AddConsumers(assembly);
                }

                config.UsingRabbitMq((context, conf) =>
                {
                    conf.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:Username"]!);
                        host.Password(configuration["MessageBroker:Password"]!);
                    });
                    conf.ConfigureEndpoints(context);
                });
            });
            
            return services;
        }
    }
}
