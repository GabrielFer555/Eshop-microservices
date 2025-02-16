
namespace Ordering.Infrastructure.Data.Configuration
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasConversion(x => x.Value, orderItemId => OrderItemId.Of(orderItemId));

			builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);	//1 to many relation (one product to many orderitems) 

			builder.Property(x=> x.Price).HasPrecision(18, 2).IsRequired();
			builder.Property(x => x.Quantity).IsRequired();
		}
	}
}
