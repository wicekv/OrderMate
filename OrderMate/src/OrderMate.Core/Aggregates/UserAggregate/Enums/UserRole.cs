namespace OrderMate.Core.Aggregates.UserAggregate.Enums;

public class UserRole : SmartEnum<UserRole>
{
  public static readonly UserRole Customer = new(nameof(Customer), 1);
  public static readonly UserRole Admin = new(nameof(Admin), 2);

  private UserRole(string name, int value) : base(name, value) { }
}
