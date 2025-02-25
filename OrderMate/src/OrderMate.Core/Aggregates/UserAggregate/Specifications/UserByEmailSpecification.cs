using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Core.Aggregates.UserAggregate.Specifications;
public class UserByEmailSpecification : Specification<User>
{
  public UserByEmailSpecification(string email)
  {
    Query.Where(u => u.Email == email);
  }
}
