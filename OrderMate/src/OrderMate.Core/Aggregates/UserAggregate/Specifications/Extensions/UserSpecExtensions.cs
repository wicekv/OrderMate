using OrderMate.Core.Aggregates.UserAggregate.Filters;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.Core.Aggregates.UserAggregate.Specifications.Extensions;

public static class UserSpecExtensions
{
  public static ISpecificationBuilder<User> ApplyOrdering(this ISpecificationBuilder<User> builder, BaseFilter? filter = null)
  {
    if (filter is null) return builder.OrderBy(x => x.Id);

    var isAscending = !(filter.OrderBy?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false);

    return filter.SortBy switch
    {
      nameof(User.Name) => isAscending ? builder.OrderBy(x => x.Name) : builder.OrderByDescending(x => x.Name),
      nameof(User.Email) => isAscending ? builder.OrderBy(x => x.Email) : builder.OrderByDescending(x => x.Email),
      _ => builder.OrderBy(x => x.Id)
    };
  }
}
