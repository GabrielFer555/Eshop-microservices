
namespace Ordering.Domain.Abstractions
{
	public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId> // create a base class for aggregates
	{
		private readonly List<IDomainEvent> _domainEvents = new();

		public IReadOnlyList<IDomainEvent> DomainEvents =>_domainEvents.AsReadOnly();
		public void AddDomainEvents(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}
		public IDomainEvent[] ClearDomainEvents()
		{
			var deQueuedEvents = _domainEvents.ToArray();
			_domainEvents.Clear();
			return deQueuedEvents;
		}
	}
}
