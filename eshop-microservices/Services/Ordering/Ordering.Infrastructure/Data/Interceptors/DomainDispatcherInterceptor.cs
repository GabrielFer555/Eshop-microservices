
namespace Ordering.Infrastructure.Data.Interceptors
{
	public class DomainDispatcherInterceptor(IMediator mediator):SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
			return base.SavingChanges(eventData, result);
		}
		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			await DispatchDomainEvents(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}
		private async Task DispatchDomainEvents(DbContext? dbContext)
		{
			if (dbContext == null) return;
			foreach ( var entry in dbContext.ChangeTracker.Entries<IAggregate>().Where(e => e.Entity.DomainEvents.Any()))
			{
				//dispatchEvent
				IDomainEvent[] eventsToBeDispatched = entry.Entity.ClearDomainEvents();
				foreach (var domainEvent in eventsToBeDispatched) {
					await mediator.Publish(domainEvent);
				}


			}
		}
	}
}
