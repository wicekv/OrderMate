using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Infrastructure.Data;

public static class SeedData
{
  public static readonly User user1 = new("wicek", "test@wp.pl", UserRole.Admin);
  public static readonly User user2 = new("wicek2", "test2@wp.pl", UserRole.Admin);

  public static async Task InitializeAsync(AppDbContext dbContext)
  {
    if (await dbContext.Users.AnyAsync()) return;

    await PopulateTestDataAsync(dbContext);
  }

  public static async Task PopulateTestDataAsync(AppDbContext dbContext)
  {
    dbContext.Users.AddRange(user1, user2);
    await dbContext.SaveChangesAsync();
  }
}
