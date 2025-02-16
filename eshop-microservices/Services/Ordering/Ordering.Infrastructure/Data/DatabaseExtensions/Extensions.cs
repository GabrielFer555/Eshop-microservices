

namespace Ordering.Infrastructure.Data.DatabaseExtensions
{
    public static class Extensions
    {
        public static async Task<WebApplication> UseAutoMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedDatabaseAsync(context);
            return app;
        }

        public static async Task SeedDatabaseAsync(ApplicationDbContext context)
        {
            await SeedCustomersAsync(context);
            await SeedProductsAsync(context);
            await SeedOrdersWithItemsAsync(context);
        }
		private static async Task SeedCustomersAsync(ApplicationDbContext context)
        {
            if(!await context.Customers.AnyAsync()) // verifies to check if there is any data on the database
            {
                foreach(var customerId in InitialData.Customers)
                {
                    await context.Customers.AddAsync(customerId);
                    await context.SaveChangesAsync();
                }
            }
        }
        private static async Task SeedProductsAsync(ApplicationDbContext context)
        {
			if (!await context.Products.AnyAsync())
			{
				await context.Products.AddRangeAsync(InitialData.Products); //allows you to add an array of data
				await context.SaveChangesAsync();
			}
		}
        private static async Task SeedOrdersWithItemsAsync(ApplicationDbContext context)
        {
            if(!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
