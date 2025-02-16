using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data
{
	public class ApplicationDbContext:DbContext, IApplicationDbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Product> Products => Set<Product>();
		public DbSet<Order> Orders => Set<Order>(); 
		public DbSet<OrderItem> OrdersItems => Set<OrderItem>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// builder.Entity<Customer>().Property(x => x.Name).IsRequired().HasMaxLength(100); // define the property as required in the database

			
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			
		}
	}
}
