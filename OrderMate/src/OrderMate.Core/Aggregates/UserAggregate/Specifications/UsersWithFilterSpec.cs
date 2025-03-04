using OrderMate.Core.Aggregates.UserAggregate.Filters;
using OrderMate.Core.Aggregates.UserAggregate.Specifications.Extensions;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Core.Aggregates.UserAggregate.Specifications;

public sealed class UsersWithFilterSpec : Specification<User>
{
  public UsersWithFilterSpec(CustomFilter filter)
  {
    if (!string.IsNullOrWhiteSpace(filter.Role))
    {
      Query.Where(u => u.Role.Name == filter.Role);
    }

    Query.ApplyOrdering(filter);

    if (filter.Page.HasValue && filter.PageSize.HasValue)
    {
      Query.Skip((filter.Page.Value - 1) * filter.PageSize.Value)
           .Take(filter.PageSize.Value);
    }
  }
}
