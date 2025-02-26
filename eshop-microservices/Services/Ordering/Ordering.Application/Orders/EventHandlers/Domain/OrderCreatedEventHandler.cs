using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {event} ", domainEvent.GetType().Name);
<<<<<<< HEAD
            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
				var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
				await publishEndpoint.Publish(orderCreatedIntegrationEvent);
			}
=======

            if(await featureManager.IsEnabledAsync("OrderFullfilment")){
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();

                await publishEndpoint.Publish(orderCreatedIntegrationEvent);
            }
            
>>>>>>> 88c58e6d28dfdff51a8ae8cd273f1d5d04824a7a
        }

    }
}
