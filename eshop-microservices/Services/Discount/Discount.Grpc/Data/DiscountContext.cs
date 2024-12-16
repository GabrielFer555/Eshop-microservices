using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public class DiscountContext: DbContext
	{
		public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }

		public DbSet<Coupon> Coupons { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//seeding data
			modelBuilder.Entity<Coupon>().HasData(
				new Coupon(){Id = 1, ProductName = "IPhone X", Description="IPhone Discount", Amount =135},
				new Coupon() { Id=2, ProductName="Samsung TV", Description = "Samsung TV Discount", Amount=112}
				);
		
		}
	}
}
