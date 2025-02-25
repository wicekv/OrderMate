using OrderMate.Core.Aggregates.OrderAggregate.Enums;
using OrderMate.Core.Aggregates.OrderAggregate;

namespace OrderMate.Infrastructure.Data.Config.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("Orders");
    builder.HasKey(o => o.Id);

    builder.Property(o => o.UserId)
        .IsRequired();

    builder.Property(o => o.Status)
        .HasConversion(status => status.Name, name => OrderStatus.FromName(name, false))
        .IsRequired();

    builder.HasMany(o => o.OrderItems)
        .WithOne()
        .HasForeignKey(oi => oi.Id)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
