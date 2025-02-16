
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configuration
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(oi => oi.Id).HasConversion(x => x.Value, dbId => OrderId.Of(dbId));

			builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();

			builder.HasMany<OrderItem>().WithOne().HasForeignKey(x => x.OrderId);

			builder.ComplexProperty(o => o.OrderName, nameBuilder =>
			{
				nameBuilder.Property(x => x.Value).
				HasColumnName(nameof(Order.OrderName)).
				HasMaxLength(100).
				IsRequired();
			});

			builder.ComplexProperty(ord => ord.ShippingAddress, nameBuilder =>
			{
				nameBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
				nameBuilder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
				nameBuilder.Property(x => x.EmailAddress).HasMaxLength(50);
				nameBuilder.Property(x => x.AddressLine).HasMaxLength(180).IsRequired();
				nameBuilder.Property(x => x.Country).HasMaxLength(50);
				nameBuilder.Property(x => x.State).HasMaxLength(50);
				nameBuilder.Property(x => x.ZipCode).HasMaxLength(5).IsRequired();

			});
			builder.ComplexProperty(ord => ord.BillingAddress, nameBuilder =>
			{
				nameBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
				nameBuilder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
				nameBuilder.Property(x => x.EmailAddress).HasMaxLength(50);
				nameBuilder.Property(x => x.AddressLine).HasMaxLength(180).IsRequired();
				nameBuilder.Property(x => x.Country).HasMaxLength(50);
				nameBuilder.Property(x => x.State).HasMaxLength(50);
				nameBuilder.Property(x => x.ZipCode).HasMaxLength(5).IsRequired();

			});
			builder.ComplexProperty(ord => ord.Payment, nameBuilder =>
			{
				nameBuilder.Property(x => x.CardNumber).HasMaxLength(50);
				nameBuilder.Property(x => x.CardNumber).HasMaxLength(24).IsRequired();
				nameBuilder.Property(x => x.Expiration).HasMaxLength(10);
				nameBuilder.Property(x => x.CVV).HasMaxLength(3);
				nameBuilder.Property(x => x.PaymentMethod).HasMaxLength(50);
				
			});
			builder.Property(x => x.OrderStatus).HasDefaultValue(OrderStatus.Draft).HasConversion(s => s.ToString(), dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));

			builder.Property(x => x.TotalPrice).HasPrecision(18, 2);
		}
	}
}
