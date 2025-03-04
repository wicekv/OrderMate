using Bogus;
using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.UnitTests.TestHelpers.Fakes;
public static class FakeUserFactory
{
  private static readonly Faker _faker = new();

  private static readonly UserRole[] _roles =
  {
        UserRole.Customer,
        UserRole.Admin
    };

  public static User CreateUser(
      int? id = null,
      string? name = null,
      string? email = null,
      UserRole? role = null)
  {
    var user = new User(
        name ?? _faker.Name.FullName(),
        email ?? _faker.Internet.Email(),
        role ?? _faker.PickRandom(_roles)
    );

    if (id.HasValue)
    {
      typeof(User).GetProperty(nameof(User.Id))!.SetValue(user, id.Value);
    }

    return user;
  }
}
