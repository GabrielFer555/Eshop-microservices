
namespace Ordering.Infrastructure.Data.Interceptors
{
	public class CustomSaveChangesInterceptor: SaveChangesInterceptor
	{

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdatingToDatabase(eventData.Context);

			
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdatingToDatabase(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
		public void UpdatingToDatabase(DbContext? context)
		{
			if (context is null) return;

			foreach (var entry in context.ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.HasChangedOwnedEntity()))
			{
				if (entry.Entity is IEntity entity)
				{
					if (entry.State == EntityState.Added)
					{
						entity.CreatedAt = DateTime.UtcNow;
						entity.LastModified = DateTime.UtcNow;
						entity.CreatedBy = "Gabriel"; // implement different logic if you need to include the user who changed it
					}
					if (entry.State == EntityState.Modified)
					{
						entity.LastModified = DateTime.UtcNow;
						entity.LastModifiedBy = "Gabriel"; // implement different logic if you need to include the user who changed itw
					}
				}
			}
		}
	}

	public static class SavingChangesExtensions
	{
		public static bool HasChangedOwnedEntity(this EntityEntry entry)
		{
			return entry.References.Any(r => r.TargetEntry is not null && 
			r.TargetEntry.Metadata.IsOwned() &&
			(r.EntityEntry.State == EntityState.Added || r.EntityEntry.State == EntityState.Modified)
			);
		}
	}
	
}
