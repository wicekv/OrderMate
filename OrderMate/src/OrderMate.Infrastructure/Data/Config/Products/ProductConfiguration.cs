using OrderMate.Core.Aggregates.ProductAggregate.Enums;
using OrderMate.Core.Aggregates.ProductAggregate;

namespace OrderMate.Infrastructure.Data.Config.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products", "product");
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(p => p.Price)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(p => p.Stock)
        .IsRequired();

    builder.Property(p => p.Category)
        .HasConversion(category => category.Name, name => ProductCategory.FromName(name, false))
        .IsRequired();
  }
}
