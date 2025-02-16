using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
	public interface IApplicationDbContext
	{
		DbSet<Customer> Customers { get; }
		DbSet<Order> Orders { get; }
		DbSet<OrderItem> OrdersItems { get; }
		DbSet<Product> Products { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
