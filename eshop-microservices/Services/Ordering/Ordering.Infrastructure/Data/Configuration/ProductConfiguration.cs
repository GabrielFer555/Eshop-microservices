namespace Ordering.Infrastructure.Data.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasConversion(x => x.Value, productId => ProductId.Of(productId));
			builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
			builder.Property(x => x.Price).HasPrecision(18, 2);
		}
	}
}
