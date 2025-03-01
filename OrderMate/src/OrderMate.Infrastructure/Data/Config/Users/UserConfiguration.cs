using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Infrastructure.Data.Config.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users", "user");
    builder.HasKey(u => u.Id);

    builder.Property(u => u.Name)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(u => u.Email)
        .IsRequired()
        .HasMaxLength(150);

    builder.Property(u => u.Role)
            .HasConversion(
                role => role.Name,
                name => UserRole.FromName(name, false))
            .IsRequired();
  }
}
