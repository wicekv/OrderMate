using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Core.Aggregates.UserAggregate.Specifications;

public class UserWithOrdersSpec : Specification<User>
{
  public UserWithOrdersSpec(int userId)
  {
    Query
        .Where(u => u.Id == userId)
        .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems);
  }
}
