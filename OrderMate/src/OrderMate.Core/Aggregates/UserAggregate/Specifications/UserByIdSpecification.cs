using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Core.Aggregates.UserAggregate.Specifications;

public class UserByIdSpecification : Specification<User>
{
  public UserByIdSpecification(int userId)
  {
    Query.Where(u => u.Id == userId);
  }
}
