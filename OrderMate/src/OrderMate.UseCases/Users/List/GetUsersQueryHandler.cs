namespace OrderMate.UseCases.Users.List;
public sealed record GetUsersQuery(int PageNumber, int PageSize) : IQuery<PagedResult<List<UserDto>>>;

public sealed class GetUsersQueryHandler(IGetUsersQueryService userQueryService) : IQueryHandler<GetUsersQuery, PagedResult<List<UserDto>>>
{
  public async Task<PagedResult<List<UserDto>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
  {
    var users = await userQueryService.GetUsersPaginatedAsync(query.PageNumber, query.PageSize, cancellationToken);

    return users;
  }
}
